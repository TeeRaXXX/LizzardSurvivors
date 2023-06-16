using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct ActiveSkillSO
{
    public SkillType SkillType;
    public GameObject SkillPrefab;
}


[CreateAssetMenu(fileName = "SOActiveSkills", menuName = "NastyDoll/New Active Skills List")]
public class SOActiveSkills : ScriptableObject
{
    public List<ActiveSkillSO> ActiveSkillsList;
}