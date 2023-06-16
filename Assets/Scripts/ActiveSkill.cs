using UnityEngine;

public abstract class ActiveSkill : Skill, IUpgradable
{
     [SerializeField] protected int MaxLevel = 8;
     [SerializeField] protected int CurrentLevel = 0;
     [SerializeField] protected SkillType SkillType;

    public void Upgrade()
    {
        if (CurrentLevel < MaxLevel)
            CurrentLevel++;
        else return;
    }
}