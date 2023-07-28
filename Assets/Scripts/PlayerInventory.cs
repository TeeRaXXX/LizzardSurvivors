using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private SkillsSpawner _skillsSpawner;
    private static int _maximumSkillsCount = 12;
    private List<SkillsEvolutionSO> _skillsEvolutionList;

    public static Dictionary<SkillType, int> Skills { get; private set; }
    public static int Coins { get; private set; } = 0;
    public static int Level { get; private set; } = 1;
    public static int LevelPoints { get; private set; } = 0;

    public void InitInventory(SkillType baseSkill, SkillsSpawner skillsSpawner, int coins = 0)
    {
        Skills = new Dictionary<SkillType, int>();
        EventManager.OnLevelUp.AddListener(OnLevelUp);
        EventManager.OnSkillAdded.AddListener(AddSkill);
        EventManager.OnChestPickUp.AddListener(OnChestPickUp);
        _skillsSpawner = skillsSpawner;
        _skillsEvolutionList = skillsSpawner.GetSkillsEvolutionList();
        Coins = coins;
        LevelPoints = 0;
        Level = 1;
        _skillsSpawner.SpawnSkill(baseSkill, 1, out bool isMaxLevel);
    }

    public void AddSkill(SkillType skill, int level)
    {
        if (!Skills.ContainsKey(skill))
        {
            Skills.Add(skill, level);
            Debug.Log(skill.ToString() + Skills[skill].ToString());
        }
        else
        {
            Skills[skill]++; //increase level
            Debug.Log(skill.ToString() + Skills[skill].ToString());
        }
    }

    public void UpdateMaxSkillsCount(int skillsCount)
    {
        _maximumSkillsCount = skillsCount;
    }

    private void OnLevelUp(int levelsToAdd)
    {
        Level += levelsToAdd;
        GetNewSkills();
    }

    private void GetNewSkills(int skillsCount = 3)
    {
        List<SkillType> avoidSkills = new List<SkillType>();

        foreach (var skill in Skills)
            if (skill.Value == _skillsSpawner.GetMaxLevelOfSkill(skill.Key))
                avoidSkills.Add(skill.Key);

        foreach (var skill in _skillsSpawner.GetEvolvedSkills())
            avoidSkills.Add(skill);

        List<SkillType> skills = new List<SkillType>(GameDataStorage.Instance.GameData.GetDiscoveredSkillsInstead(avoidSkills));
        List<SkillType> newSkills = new List<SkillType>();
        
        if (skills.Count < skillsCount)
            skillsCount = skills.Count;

        for (int i = 0; i < skillsCount; i++)
        {
            int index = Random.Range(0, skills.Count);
            newSkills.Add(skills[index]);
            skills.Remove(skills[index]);
        }

        EventManager.OnNewSkillsOfferEvent(newSkills);
    }

    private void OnChestPickUp()
    {
        Coins += GetGold();

        bool isEvolved = TryCheckToEvolve();

        if (!isEvolved)
            foreach (var skill in Skills)
                if (_skillsSpawner.IsEvolutionSkill(skill.Key) && _skillsSpawner.GetMaxLevelOfSkill(skill.Key) != skill.Value)
                {
                    int skillLevel = skill.Value + 1;
                    _skillsSpawner.SpawnSkill(skill.Key, skillLevel, out bool isMaxLevel);
                    return;
                }
        
    }

    private void AddEvolvedSkill(SkillType skill, int level, List<SkillType> skillsToDelete)
    {
        foreach (var skillToDelete in skillsToDelete)
        {
            Skills.Remove(skillToDelete);
            EventManager.OnSkillDeletedEvent(skillToDelete, _skillsSpawner.GetCurrentSkillLevel(skillToDelete));
            _skillsSpawner.DeleteSkill(skillToDelete);
        }

        _skillsSpawner.SpawnSkill(skill, level, out bool isMaxLevel);
    }

    private bool TryCheckToEvolve()
    {
        foreach (var firstSkill in Skills)
        {
            if (firstSkill.Value == _skillsSpawner.GetMaxLevelOfSkill(firstSkill.Key))
            {
                SkillType maxedSkill = firstSkill.Key;

                foreach (var secondSkill in Skills)
                {
                    foreach (var evolvedSkill in _skillsEvolutionList)
                    {
                        if ((evolvedSkill.FirstSkill == maxedSkill && evolvedSkill.SecondSkill == secondSkill.Key) ||
                             evolvedSkill.FirstSkill == secondSkill.Key && evolvedSkill.SecondSkill == maxedSkill)
                        {
                            int firstSkillLevel = _skillsSpawner.GetCurrentSkillLevel(firstSkill.Key);
                            int secondSkillLevel = _skillsSpawner.GetCurrentSkillLevel(secondSkill.Key);
                            int maxLevel = firstSkillLevel < secondSkillLevel ? firstSkillLevel : secondSkillLevel;
                            AddEvolvedSkill(evolvedSkill.EvolvedSkill, maxLevel, evolvedSkill.SkillsToDeleteAfterEvolution);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private int GetGold()
    {
        return 10;
    }
}