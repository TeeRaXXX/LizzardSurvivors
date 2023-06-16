using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillsHandler : MonoBehaviour
{
    [SerializeField] private SOActiveSkills _activeSkillsSO;
    [SerializeField] private Transform _skillsHolder;

    private Dictionary<SkillType, GameObject> _activeSkills;

    public void ActiveSkillsInit(SOCharacter character)
    {
        _activeSkills = InitSkillsDictionary(_activeSkillsSO.ActiveSkillsList);
        AddNewSkill(GetActiveSkill(character.BaseActiveSkill), character.BaseActiveSkill);
    }
    
    private void AddNewSkill(GameObject skillPrefab, SkillType skillType)
    {
        Instantiate(skillPrefab, _skillsHolder);
        EventManager.OnSkillAddedEvent(skillType);
    }

    private GameObject GetActiveSkill(SkillType skillType)
    {
        return _activeSkills[skillType];
    }

    private Dictionary<SkillType, GameObject> InitSkillsDictionary(List<ActiveSkillSO> activeSkills)
    {
        Dictionary<SkillType, GameObject> temp = new Dictionary<SkillType, GameObject>();

        foreach (var skill in activeSkills)
        {
            temp.Add(skill.SkillType, skill.SkillPrefab);
        }

        return temp;
    }
}