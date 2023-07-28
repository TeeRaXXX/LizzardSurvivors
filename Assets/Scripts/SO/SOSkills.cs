using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct SkillSO
{
    public SkillType SkillType;
    public string Name;
    public string Description;
    public int MaxLevel;
    public bool IsActive;
    public bool IsEvolution;
    public GameObject SkillPrefab;
    public Sprite SkillLogo;
}

[Serializable] public struct SkillsEvolutionSO
{
    public SkillType FirstSkill;
    public SkillType SecondSkill;
    public SkillType EvolvedSkill;
    public List<SkillType> SkillsToDeleteAfterEvolution;
}

[CreateAssetMenu(fileName = "SOSkills", menuName = "NastyDoll/New Skills List")]
public class SOSkills : ScriptableObject
{
    public List<SkillSO> SkillsList;
    public List<SkillsEvolutionSO> SkillsEvolutionList;
}