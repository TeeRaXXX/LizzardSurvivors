public static class SkillsLibrary
{
    public static ActiveSkill GetActiveSkill(SkillType skill)
    {
        switch (skill)
        {
            case SkillType.Music:
                return new SkillMusic();

            case SkillType.Idol:
                return null;

            default:
                return null;
        }
    }

    public static ActiveSkill GetPassiveSkill(SkillType skill)
    {
        switch (skill)
        {
            case SkillType.Respawn:
                return null;

            case SkillType.IncreaseProjectileCount:
                return null;

            default:
                return null;
        }
    }

    public static Skill GetSkill(SkillType skill)
    {
        switch (skill)
        {
            case SkillType.Music:
                return new SkillMusic();

            case SkillType.Idol:
                return null;

            case SkillType.Respawn:
                return null;

            case SkillType.IncreaseProjectileCount:
                return null;

            default:
                return null;
        }
    }
}