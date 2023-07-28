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

    public void SpawnSkill(SkillType skillType, int level, out bool isMaxLevel)
    {
        isMaxLevel = false;

        if (!_skills.ContainsKey(skillType))
        {
            GameObject skillPrefab = Instantiate(GetSkillPrefab(skillType), _skillsHolder);
            _skills.Add(skillType, skillPrefab);
            _skillsLevels.Add(skillType, level);

            if (_skillsLevels[skillType] == GetMaxLevelOfSkill(skillType))
                isMaxLevel = true;

            if (IsEvolutionSkill(skillType))
                skillPrefab.GetComponent<IEvolvedSkill>().Initialize(level);


            EventManager.OnSkillAddedEvent(skillType, level);
        }
        else
        {
            if (_skillsLevels[skillType] == GetMaxLevelOfSkill(skillType))
            {
                isMaxLevel = true;
                return;
            }

            _skillsLevels[skillType]++;
            _skills[skillType].GetComponent<IUpgradable>().Upgrade(true);
            EventManager.OnSkillAddedEvent(skillType, level);
        }
    }

    public bool IsEvolutionSkill(SkillType skillType) 
    {
        foreach (var skill in _skillsSO.SkillsList)
            if (skill.SkillType == skillType)
                if (skill.IsEvolution)
                    return true;

        return false;
    }

    private GameObject GetSkillPrefab(SkillType skillType)
    {
        foreach (var skill in _skillsSO.SkillsList)
            if (skill.SkillType == skillType)
                return skill.SkillPrefab;
        
        return null;
    }

    public List<SkillType> GetEvolvedSkills()
    {
        List<SkillType> skills = new List<SkillType>();

        foreach (var skill in _skillsSO.SkillsList)
            if (skill.IsEvolution)
                skills.Add(skill.SkillType);
        
        return skills;
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

    public List<SkillsEvolutionSO> GetSkillsEvolutionList()
    {
        return new List<SkillsEvolutionSO>(_skillsSO.SkillsEvolutionList);
    }

    public void DeleteSkill(SkillType skillToDelete)
    {
        Destroy(_skills[skillToDelete]);
        _skills.Remove(skillToDelete);
        _skillsLevels.Remove(skillToDelete);
    }
}