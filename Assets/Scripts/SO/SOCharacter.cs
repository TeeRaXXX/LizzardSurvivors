using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "NastyDoll/New Character")]
public class SOCharacter : ScriptableObject
{
    public string CharacterName;
    public string CharacterDescription;

    public Sprite CharacterBaseSprite;
    public Sprite CharacterLogo;
    public RuntimeAnimatorController CharacterAnimationController;
    public PlayerStats CharacterBaseStats;
    public GameObject CharacterPrefab;

    public CharacterType CharacterType;
    public SkillType BaseActiveSkill;
}