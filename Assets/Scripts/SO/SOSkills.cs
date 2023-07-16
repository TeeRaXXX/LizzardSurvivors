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
    public GameObject SkillPrefab;
    public Sprite SkillLogo;
}

[CreateAssetMenu(fileName = "SOSkills", menuName = "NastyDoll/New Skills List")]
public class SOSkills : ScriptableObject
{
    public List<SkillSO> SkillsList;
}