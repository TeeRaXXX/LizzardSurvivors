using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "NastyDoll/New Character")]
public class SOCharacter : ScriptableObject
{
    public string CharacterName;
    public string CharacterDescription;

    public Sprite CharacterBaseSprite;
    public RuntimeAnimatorController CharacterAnimationController;
    public PlayerStats CharacterBaseStats;

    public CharacterType CharacterType;
    public SkillType BaseActiveSkill;
}