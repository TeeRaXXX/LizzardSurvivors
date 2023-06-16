public abstract class PassiveSkill : Skill, IUpgradable
{
    public int MaxLevel { get; private set; } = 8;
    public int CurrentLevel { get; private set; } = 0;
    public abstract SkillType SkillType { get; }

    public void Upgrade()
    {
        if (CurrentLevel < MaxLevel)
            CurrentLevel++;
    }
}