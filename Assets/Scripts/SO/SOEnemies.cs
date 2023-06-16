using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct EnemySO
{
    public EnemyType EnemyType;
    public GameObject EnemyPrefab;
}

[CreateAssetMenu(fileName = "SOEnemiesList", menuName = "NastyDoll/New Enemies List")]
public class SOEnemies : ScriptableObject
{
    public List<EnemySO> EnemiesList;
}