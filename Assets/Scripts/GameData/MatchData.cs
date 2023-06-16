using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct KilledEnemyTypeData
{
    public EnemyType Type;
    public int Count;
}

[Serializable] public class MatchData
{
    [SerializeField] private int _reachedLevel;
    [SerializeField] private DateTime _playedDate;
    [SerializeField] private List<KilledEnemyTypeData> _killedEnemies;
}