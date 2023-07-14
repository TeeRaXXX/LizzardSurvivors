using System.Collections.Generic;

public class PlayerInventory
{
    private SkillsHandler _skillsHandler;
    private static int _maximumSkillsCount = 12;

    public static List<SkillType> Skills { get; private set; }
    public static int Coins { get; private set; } = 0;
    public static int Level { get; private set; } = 1;
    public static int LevelPoints { get; private set; } = 0;

    public void InitInventory(SkillType baseSkill, SkillsHandler skillsHandler, int coins = 0)
    {
        Skills = new List<SkillType>(_maximumSkillsCount);
        _skillsHandler = skillsHandler;
        AddSkill(baseSkill);
        Coins = coins;
        LevelPoints = 0;
        Level = 1;
    }

    public void AddSkill(SkillType skill)
    {
        if (Skills.Count < _maximumSkillsCount)
        {
            _skillsHandler.SpawnSkill(skill);
            Skills.Add(skill);
            EventManager.OnSkillAddedEvent(skill);
        }
    }

    public void UpdateMaxSkillsCount(int skillsCount)
    {
        _maximumSkillsCount = skillsCount;
    }
}