using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillsView : MonoBehaviour
{
    [SerializeField] private List<Image> _activeSkillSprites;
    [SerializeField] private List<Image> _passiveSkillSprites;
    [SerializeField] Sprite _skillSpritePlaceholder;
    [SerializeField] GameObject _skillsOfferScreen;
    [SerializeField] List<Image> _skillsOfferFrames;
    [SerializeField] List<TMP_Text> _skillsOfferDescriptions;

    [SerializeField] private List<SkillType> _activeSkills;
    [SerializeField] private List<SkillType> _passiveSkills;

    private SkillsSpawner _skillsHandler;
    private List<SkillType> _currentChoice;

    public void Initialize(SkillsSpawner skillsHandler)
    {
        _currentChoice = new List<SkillType>();
        _skillsOfferScreen.gameObject.SetActive(false);
        _skillsHandler = skillsHandler;
        ResetAllSkills();
        EventManager.OnSkillAdded.AddListener(AddSkill);
        EventManager.OnNewSkillsOffer.AddListener(OfferNewSkills);
    }

    private void AddSkill(SkillType skillType)
    {
        int index = 0;
        
        if (_skillsHandler.IsSkillActive(skillType) && !_activeSkills.Contains(skillType))
        {
            foreach (var skillSprite in _activeSkillSprites)
            {
                if (skillSprite.sprite == _skillSpritePlaceholder)
                {
                    skillSprite.sprite = _skillsHandler.GetSkillLogo(skillType);
                    _activeSkills[index] = skillType;
                    break;
                }

                index++;
            }
        }
        else if (!_skillsHandler.IsSkillActive(skillType) && !_passiveSkills.Contains(skillType))
        {
            foreach (var skillSprite in _passiveSkillSprites)
            {
                if (skillSprite.sprite == _skillSpritePlaceholder)
                {
                    skillSprite.sprite = _skillsHandler.GetSkillLogo(skillType);
                    _passiveSkills[index] = skillType;
                    break;
                }

                index++;
            }
        }
    }

    private void ResetAllSkills()
    {
        for (int i = 0; i < _activeSkills.Count; i++)
        {
            _activeSkillSprites[i].sprite = _skillSpritePlaceholder;
            _activeSkills[i] = SkillType.None;
        }

        for (int i = 0; i < _activeSkills.Count; i++)
        {
            _passiveSkillSprites[i].sprite = _skillSpritePlaceholder;
            _passiveSkills[i] = SkillType.None;
        }
    }

    private void OfferNewSkills(List<SkillType> skillsToOffer)
    {
        Time.timeScale = 0;
        ResetSkillsOfferWindow();
        _skillsOfferScreen.gameObject.SetActive(true);

        for (int i = 0; i < skillsToOffer.Count; i++)
        {
            _currentChoice[i] = skillsToOffer[i];
            _skillsOfferFrames[i].sprite = _skillsHandler.GetSkillLogo(skillsToOffer[i]);
            _skillsOfferDescriptions[i].text = _skillsHandler.GetSkillName(skillsToOffer[i])
                + " level " + (_skillsHandler.GetCurrentSkillLevel(skillsToOffer[i]) + 1);
        }
    }

    private void ResetSkillsOfferWindow()
    {
        _currentChoice = new List<SkillType>();

        for (int i = 0; i < _skillsOfferFrames.Count; i++)
        {
            _currentChoice.Add(SkillType.None);
            _skillsOfferFrames[i].sprite = _skillSpritePlaceholder;
            _skillsOfferDescriptions[i].text = string.Empty;
        }
    }

    public void OnNewSkillClick(int skillNumber)
    {
        if (_currentChoice[skillNumber] == SkillType.None)
            return;

        _skillsHandler.SpawnSkill(_currentChoice[skillNumber], out bool isMaxLevel);
        _skillsOfferScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void SkipSkillChoice()
    {
        _skillsOfferScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}