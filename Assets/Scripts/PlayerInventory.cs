using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private SkillsSpawner _skillsSpawner;
    private List<SkillsEvolutionSO> _skillsEvolutionList;
    private List<SkillType> _skillsToAvoid;
    private int skillsToOfferCount;
    private int _playerIndex;

    public Dictionary<SkillType, int> Skills { get; private set; }
    public int Coins { get; private set; } = 0;
    public int Level { get; private set; } = 1;
    public int LevelPoints { get; private set; } = 0;

    public void InitInventory(SkillType baseSkill, SkillsSpawner skillsSpawner, int playerIndex, int coins = 0)
    {
        _playerIndex = playerIndex;
        Skills = new Dictionary<SkillType, int>();
        _skillsToAvoid = new List<SkillType>();
        EventManager.OnLevelUp.AddListener(OnLevelUp);
        EventManager.OnSkillAdded.AddListener(AddSkill);
        EventManager.OnChestPickUp.AddListener(OnChestPickUp);
        _skillsSpawner = skillsSpawner;
        _skillsEvolutionList = skillsSpawner.GetSkillsEvolutionList();
        Coins = coins;
        skillsToOfferCount = 3;
        LevelPoints = 0;
        Level = 1;
        _skillsSpawner.SpawnSkill(baseSkill, 1, _playerIndex, out bool isMaxLevel);
        Debug.Log($"Player's {playerIndex} inventory was initialized");
    }

    public void AddSkill(SkillType skill, int level, int playerIndex)
    {
        if (playerIndex != _playerIndex)
            return;

        if (!Skills.ContainsKey(skill))
        {
            Skills.Add(skill, level);
        }
        else
        {
            Skills[skill]++; //increase level
        }
    }

    public bool ContainsSkill(SkillType skillType) => Skills.ContainsKey(skillType);

    public int GetSkillLevel(SkillType skillType) => Skills.TryGetValue(skillType, out int level) ? level : 0;

    public bool IsSkillOnMaxLevel(SkillType skillType) => Skills[skillType] == _skillsSpawner.GetMaxLevelOfSkill(skillType);

    private void OnLevelUp(int levelsToAdd)
    {
        Level += levelsToAdd;
        GetNewSkills();
    }

    public List<SkillType> GetNewSkills()
    {
        List<SkillType> avoidSkills = new List<SkillType>();

        foreach (var skill in Skills)
            if (skill.Value == _skillsSpawner.GetMaxLevelOfSkill(skill.Key))
                avoidSkills.Add(skill.Key);

        foreach (var skill in _skillsSpawner.GetEvolvedSkills())
            avoidSkills.Add(skill);

        foreach (var skill in _skillsToAvoid)
            avoidSkills.Add(skill);

        List<SkillType> skills = new List<SkillType>(GameDataStorage.Instance.GameData.GetDiscoveredSkillsInstead(avoidSkills));
        List<SkillType> newSkills = new List<SkillType>();

        int skillsCount = skillsToOfferCount;

        if (skills.Count < skillsToOfferCount)
            skillsCount = skills.Count;

        for (int i = 0; i < skillsCount; i++)
        {
            int index = Random.Range(0, skills.Count);
            newSkills.Add(skills[index]);
            skills.Remove(skills[index]);
        }

        return newSkills;
    }

    private void OnChestPickUp(int playerIndex)
    {
        Coins += GetGold();

        bool isEvolved = TryCheckToEvolve();

        if (!isEvolved)
            foreach (var skill in Skills)
                if (_skillsSpawner.IsEvolutionSkill(skill.Key) && _skillsSpawner.GetMaxLevelOfSkill(skill.Key) != skill.Value)
                {
                    int skillLevel = skill.Value + 1;
                    _skillsSpawner.SpawnSkill(skill.Key, skillLevel, _playerIndex, out bool isMaxLevel);
                    return;
                }
    }

    private void AddEvolvedSkill(SkillType skill, int level, List<SkillType> skillsToDelete)
    {
        foreach (var skillToDelete in skillsToDelete)
        {
            EventManager.OnSkillDeletedEvent(skillToDelete, Skills[skillToDelete], _playerIndex);
            Skills.Remove(skillToDelete);
            _skillsSpawner.DeleteSkill(skillToDelete, _playerIndex);
            _skillsToAvoid.Add(skillToDelete);
        }

        _skillsSpawner.SpawnSkill(skill, level, _playerIndex, out bool isMaxLevel);
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
                            int firstSkillLevel = Skills[firstSkill.Key];
                            int secondSkillLevel = Skills[secondSkill.Key];
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