using UnityEngine;

public class SkillsHandler : MonoBehaviour
{
    [SerializeField] private SOSkills _skillsSO;
    [SerializeField] private Transform _skillsHolder;

    public void SpawnSkill(SkillType skillType)
    {
        Instantiate(GetSkillPrefab(skillType), _skillsHolder);
    }

    private GameObject GetSkillPrefab(SkillType skillType)
    {
        foreach (var skill in _skillsSO.SkillsList)
            if (skill.SkillType == skillType)
                return skill.SkillPrefab;
        
        return null;
    }

    public Sprite GetSkillLogo(SkillType skillType) 
    {
        foreach (var skill in _skillsSO.SkillsList)
            if (skill.SkillType == skillType)
                return skill.SkillLogo;

        return null;
    }

    public bool IsSkillActive(SkillType skillType) 
    {
        foreach (var skill in _skillsSO.SkillsList)
            if (skill.SkillType == skillType)
                return skill.IsActive;

        return true;
    }
}