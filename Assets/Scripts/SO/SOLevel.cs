using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct EnemiesPerMinute
{
    public int Minute;
    public float SpawnFrequency;
    public EnemyType Boss;
    public List<EnemiesPercent> EnemiesPercents;
}

[Serializable] public struct EnemiesPercent
{
    public EnemyType Enemy;
    public float Percent;
}

[CreateAssetMenu(fileName = "LevelSO", menuName = "NastyDoll/New Level")]
public class SOLevel : ScriptableObject
{
    public string LevelName;
    public string LevelDescription;

    public List<EnemiesPerMinute> EnemiesWaves;
}