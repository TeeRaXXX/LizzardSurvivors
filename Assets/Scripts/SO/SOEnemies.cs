using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOEnemiesList", menuName = "NastyDoll/New Enemies List")]
[Serializable] public class SOEnemies : ScriptableObject
{
    public List<SOEnemy> EnemiesList;
}