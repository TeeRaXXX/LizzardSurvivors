using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private static int _maximumSkillsCount = 8;

    public static List<ActiveSkill> ActiveSkills { get; private set; } = new List<ActiveSkill>(_maximumSkillsCount);
    public static List<PassiveSkill> PassiveSkills { get; private set; } = new List<PassiveSkill>(_maximumSkillsCount);
    public static int Coins { get; private set; } = 0;
    public static int Level { get; private set; } = 1;
    public static int LevelPoints { get; private set; } = 0;

    public void InitInventory(SkillType baseSkill, int coins = 0)
    {
        AddSkill(SkillsLibrary.GetActiveSkill(baseSkill));
        Coins = coins;
        LevelPoints = 0;
        Level = 1;
    }

    public void GetLevelPoints(int pointsCount, GameObject pointsSource)
    {
        if (pointsCount > 0)
        {
            LevelPoints += pointsCount;
            EventManager.OnSkillPointsAddedEvent(pointsCount, LevelPoints);
            LevelUp();
        }
        else throw new Exception($"Exception: {pointsSource.name} try give {pointsCount} points to player!");
    }

    private void LevelUp() 
    {
        if (LevelPoints >= Level * 100)
        {
            Level++;
            EventManager.OnLevelUpEvent(Level);
        }
    }

    public void AddSkill(ActiveSkill skill)
    {
        if (ActiveSkills.Count < _maximumSkillsCount)
        {
            ActiveSkills.Add(skill);
            //EventManager.OnSkillAddedEvent(skill);
        }
    }

    public void AddSkill(PassiveSkill skill)
    {
        if (PassiveSkills.Count < _maximumSkillsCount)
        {
            PassiveSkills.Add(skill);
            //EventManager.OnSkillAddedEvent(skill);
        }
    }
}