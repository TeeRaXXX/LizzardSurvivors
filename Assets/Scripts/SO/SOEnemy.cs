using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "NastyDoll/New Enemy")]
public class SOEnemy : ScriptableObject
{
    public string EnemyName;
    public string EnemyDescription;

    public Sprite EnemyBaseSprite;
    public AnimatorController EnemyAnimationController;
    public EnemyStats EnemyBaseStats;

    public EnemyType EnemyType;
}