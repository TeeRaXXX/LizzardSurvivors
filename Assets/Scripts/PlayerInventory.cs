using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private SkillsSpawner _skillsSpawner;
    private static int _maximumSkillsCount = 12;

    public static Dictionary<SkillType, int> Skills { get; private set; }
    public static int Coins { get; private set; } = 0;
    public static int Level { get; private set; } = 1;
    public static int LevelPoints { get; private set; } = 0;

    public void InitInventory(SkillType baseSkill, SkillsSpawner skillsSpawner, int coins = 0)
    {
        Skills = new Dictionary<SkillType, int>();
        EventManager.OnLevelUp.AddListener(OnLevelUp);
        EventManager.OnSkillAdded.AddListener(AddSkill);
        _skillsSpawner = skillsSpawner;
        Coins = coins;
        LevelPoints = 0;
        Level = 1;
        _skillsSpawner.SpawnSkill(baseSkill);
    }

    public void AddSkill(SkillType skill)
    {
        if (!Skills.ContainsKey(skill))
        {
            Skills.Add(skill, 1);
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
}