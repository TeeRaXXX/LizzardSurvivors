using System.Collections.Generic;
using UnityEngine;

public class SkillsSpawner : MonoBehaviour
{
    [SerializeField] private SOSkills _skillsSO;
    [SerializeField] private Transform _skillsHolder;

    private Dictionary<SkillType, GameObject> _skills;
    private Dictionary<SkillType, int> _skillsLevels;

    private void Awake()
    {
        _skills = new Dictionary<SkillType, GameObject>();
        _skillsLevels = new Dictionary<SkillType, int>();
    }

    public void SpawnSkill(SkillType skill, out bool isMaxLevel)
    {
        isMaxLevel = false;

        if (!_skills.ContainsKey(skill))
        {
            _skills.Add(skill, Instantiate(GetSkillPrefab(skill), _skillsHolder));
            _skillsLevels.Add(skill, 1);

            if (_skillsLevels[skill] == GetMaxLevelOfSkill(skill))
                isMaxLevel = true;

            EventManager.OnSkillAddedEvent(skill);
        }
        else
        {
            if (_skillsLevels[skill] == GetMaxLevelOfSkill(skill))
            {
                isMaxLevel = true;
                return;
            }

            _skillsLevels[skill]++;
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

    public bool IsSkillOnMaxLevel(SkillType skillType) 
    {
        return _skillsLevels[skillType] == GetMaxLevelOfSkill(skillType);
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