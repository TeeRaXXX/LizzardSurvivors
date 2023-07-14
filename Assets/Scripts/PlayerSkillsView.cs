using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillsView : MonoBehaviour
{
    [SerializeField] private List<Image> _activeSkillSprites;
    [SerializeField] private List<Image> _passiveSkillSprites;
    [SerializeField] Sprite _skillSpritePlaceholder;

    SkillsHandler _skillsHandler;
    private List<SkillType> _activeSkills;
    private List<SkillType> _passiveSkills;

    public void Initialize(SkillsHandler skillsHandler)
    {
        _skillsHandler = skillsHandler;
        ResetAllSkills();
        EventManager.OnSkillAdded.AddListener(AddSkill);
    }

    private void AddSkill(SkillType skillType)
    {
        int index = 0;
        
        if (_skillsHandler.IsSkillActive(skillType))
        {
            foreach (var skillSprite in _activeSkillSprites)
            {
                if (skillSprite.sprite == _skillSpritePlaceholder)
                {
                    skillSprite.sprite = _skillsHandler.GetSkillLogo(skillType);
                    _activeSkills[index] = skillType;
                }

                index++;
            }
        }
        else
        {
            foreach (var skillSprite in _passiveSkillSprites)
            {
                if (skillSprite.sprite == _skillSpritePlaceholder)
                {
                    skillSprite.sprite = _skillsHandler.GetSkillLogo(skillType);
                    _passiveSkills[index] = skillType;
                }

                index++;
            }
        }
    }

    private void ResetAllSkills()
    {
        foreach (var skillImage in _activeSkillSprites)
        {
            skillImage.sprite = _skillSpritePlaceholder;
        }

        foreach (var skillImage in _passiveSkillSprites)
        {
            skillImage.sprite = _skillSpritePlaceholder;
        }
    }
}