using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected Sprite SkillLogo = null;
    [SerializeField] protected string SkillName = "Skill Name";
    [SerializeField] protected string SkillDescription = "Skill Description";
}