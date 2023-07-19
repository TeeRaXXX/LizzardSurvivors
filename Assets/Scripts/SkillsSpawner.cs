using System.Collections.Generic;
using UnityEngine;

public class SkillsSpawner : MonoBehaviour
{
    [SerializeField] private SOSkills _skillsSO;
    [SerializeField] private Transform _skillsHolder;

    private Dictionary<SkillType, GameObject> _skills;

    private void Awake()
    {
        _skills = new Dictionary<SkillType, GameObject>();
    }

    public void SpawnSkill(SkillType skill)
    {
        if (!_skills.ContainsKey(skill))
        {
            _skills.Add(skill, Instantiate(GetSkillPrefab(skill), _skillsHolder));
            EventManager.OnSkillAddedEvent(skill);
        }
        else
        {
            _skills[skill].GetComponent<IUpgradable>().Upgrade(true);
            EventManager.OnSkillAddedEvent(skill);
        }
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

    public int GetMaxLevelOfSkill(SkillType skillType)
    {
        foreach (var skill in _skillsSO.SkillsList)
        {
            if (skill.SkillType == skillType)
            {
                return skill.MaxLevel;
            }
        }

        return 0;
    }

    public string GetSkillName(SkillType skillType)
    {
        foreach (var skill in _skillsSO.SkillsList)
        {
            if (skill.SkillType == skillType)
            {
                return skill.Name;
            }
        }

        return "Skill Name";
    }

    public string GetSkillDescription(SkillType skillType)
    {
        foreach (var skill in _skillsSO.SkillsList)
        {
            if (skill.SkillType == skillType)
            {
                return skill.Description;
            }
        }

        return "Skill Description";
    }

    public int GetCurrentSkillLevel(SkillType skillType)
    {
       GameObject skill = null;

        if (_skills.TryGetValue(skillType, out skill))
            return _skills[skillType].GetComponent<IUpgradable>().GetCurrentLevel();
        else return 0;
    }
}