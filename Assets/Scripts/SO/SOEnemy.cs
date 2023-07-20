using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct Drop
{
    public GameObject DropPrefab;
    public float DropChance;
}


[CreateAssetMenu(fileName = "EnemySO", menuName = "NastyDoll/New Enemy")]
public class SOEnemy : ScriptableObject
{
    public string EnemyName;
    public string EnemyDescription;

    public Sprite EnemyBaseSprite;
    public RuntimeAnimatorController EnemyAnimationController;
    public EnemyStats EnemyBaseStats;

    public EnemyType EnemyType;
    public GameObject EnemyPrefab;

    public List<Drop> Drops;

    public float ExperienceMin;
    public float ExperienceMax;
}