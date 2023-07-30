using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] public struct BuffDebuffInfoSO
{
    public Sprite Logo;
    public string Name;
    public string Description;
}

[CreateAssetMenu(fileName = "BuffDebuffInfoSO", menuName = "NastyDoll/New Buff Debuff Info")]
public class SOBuffDebuffInfo : ScriptableObject
{
    public List<BuffDebuffInfoSO> BuffsDebuffs;
}