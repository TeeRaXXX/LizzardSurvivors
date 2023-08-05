using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable] public struct SkillView
{
    public GameObject SkillsListObject;
    public Image CharacterLogo;
    public List<SkillType> SkillTypes;
    public List<Image> SkillSprites;
}

public class PlayerSkillsView : MonoBehaviour
{
    [SerializeField] private List<SkillView> _activeSkills;
    [SerializeField] private List<SkillType> _passiveSkills;
    [SerializeField] private List<Image> _passiveSkillSprites;
    [SerializeField] Sprite _skillSpritePlaceholder;
    [SerializeField] Image _characterLogoSkillSelect;
    [SerializeField] GameObject _skillsOfferScreen;
    [SerializeField] List<Image> _skillsOfferFrames;
    [SerializeField] List<TMP_Text> _skillsOfferDescriptions;

    private SkillsSpawner _skillsSpawner;
    private List<SkillType> _currentChoice;
    private int _skillsToOfferCount;

    public void Initialize(SkillsSpawner skillsSpawner)
    {
        _currentChoice = new List<SkillType>();
        _skillsOfferScreen.gameObject.SetActive(false);
        _skillsSpawner = skillsSpawner;
        ResetAllSkills();

        for (int i = 0; i < skillsSpawner.PlayersCount; i++)
            _activeSkills[i].SkillsListObject.SetActive(true);

        EventManager.OnSkillAdded.AddListener(AddSkill);
        EventManager.OnSkillDeleted.AddListener(DeleteSkill);
        EventManager.OnLevelUp.AddListener(OfferNewSkills);
        EventManager.OnPlayerInitialized.AddListener(InitializePlayer);
    }

    private void InitializePlayer(PlayerCharacter playerCharacter)
    {
        _activeSkills[playerCharacter.PlayerIndex].CharacterLogo.sprite = playerCharacter.PlayerLogo;
    }

    private void AddSkill(SkillType skillType, int skillLevel, int playerIndex)
    {
        int index = 0;
        
        if (_skillsSpawner.IsSkillActive(skillType) && !_activeSkills[playerIndex].SkillTypes.Contains(skillType))
        {
            for (int i = 0; i < _activeSkills[playerIndex].SkillTypes.Count; i++)
            {
                if (_activeSkills[playerIndex].SkillSprites[i].sprite == _skillSpritePlaceholder)
                {
                    _activeSkills[playerIndex].SkillSprites[i].sprite = _skillsSpawner.GetSkillLogo(skillType);
                    _activeSkills[playerIndex].SkillTypes[i] = skillType;
                    break;
                }

                index++;
            }
        }
        else if (!_skillsSpawner.IsSkillActive(skillType) && !_passiveSkills.Contains(skillType))
        {
            foreach (var skillSprite in _passiveSkillSprites)
            {
                if (skillSprite.sprite == _skillSpritePlaceholder)
                {
                    skillSprite.sprite = _skillsSpawner.GetSkillLogo(skillType);
                    _passiveSkills[index] = skillType;
                    break;
                }

                index++;
            }
        }
    }

    private void DeleteSkill(SkillType skillType, int skillLevel, int playerIndex)
    {
        int index = 0;

        if (_skillsSpawner.IsSkillActive(skillType) && _activeSkills[playerIndex].SkillTypes.Contains(skillType))
        {
            for (int i = 0; i < _activeSkills[playerIndex].SkillTypes.Count; i++)
            {
                if (_activeSkills[playerIndex].SkillSprites[i].sprite == _skillsSpawner.GetSkillLogo(skillType))
                {
                    _activeSkills[playerIndex].SkillSprites[i].sprite = _skillSpritePlaceholder;
                    _activeSkills[playerIndex].SkillTypes[i] = SkillType.None;
                    break;
                }

                index++;
            }
        }
        else if (!_skillsSpawner.IsSkillActive(skillType) && _passiveSkills.Contains(skillType))
        {
            foreach (var skillSprite in _passiveSkillSprites)
            {
                if (skillSprite.sprite == _skillsSpawner.GetSkillLogo(skillType))
                {
                    skillSprite.sprite = _skillSpritePlaceholder;
                    _passiveSkills[index] = SkillType.None;
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
            for (int j = 0; j < _activeSkills[i].SkillTypes.Count; j++)
            {
                _activeSkills[i].SkillSprites[j].sprite = _skillSpritePlaceholder;
                _activeSkills[i].SkillTypes[j] = SkillType.None;
            }

            _activeSkills[i].SkillsListObject.SetActive(false);
        }

        for (int i = 0; i < _passiveSkills.Count; i++)
        {
            _passiveSkillSprites[i].sprite = _skillSpritePlaceholder;
            _passiveSkills[i] = SkillType.None;
        }
    }

    private void OfferNewSkills(int levelsCount)
    {
        EventManager.OnActionMapSwitchEvent(ActionMaps.UI);
        _skillsToOfferCount = levelsCount;
        OfferNewSkillsView();
    }

    private void OfferNewSkillsView()
    {
        Time.timeScale = 0;
        ResetSkillsOfferWindow();
        _skillsOfferScreen.gameObject.SetActive(true);

        var skillsToOffer = FindAnyObjectByType<PlayerCharacter>().PlayerInventory.GetNewSkills();

        for (int i = 0; i < skillsToOffer.Count; i++)
        {
            PlayerCharacter playerCharacter = GameObject.FindGameObjectsWithTag(TagsHandler.GetPlayerTag()).
                FirstOrDefault(o => o.GetComponent<PlayerCharacter>().PlayerIndex == _skillsSpawner.CurrentPlayerToSpawnSkill).GetComponent<PlayerCharacter>();
            PlayerInventory playerInventory = playerCharacter.PlayerInventory;

            _characterLogoSkillSelect.sprite = playerCharacter.PlayerLogo;
            _currentChoice[i] = skillsToOffer[i];
            _skillsOfferFrames[i].sprite = _skillsSpawner.GetSkillLogo(skillsToOffer[i]);
            _skillsOfferDescriptions[i].text = _skillsSpawner.GetSkillName(skillsToOffer[i])
                + " level " + (playerInventory.GetSkillLevel(skillsToOffer[i]) + 1);
        }

        _skillsToOfferCount--;
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

        Debug.Log(_currentChoice[skillNumber].ToString());
        PlayerInventory playerInventory = GameObject.FindGameObjectsWithTag(TagsHandler.GetPlayerTag()).
                FirstOrDefault(o => o.GetComponent<PlayerCharacter>().PlayerIndex ==
                    _skillsSpawner.CurrentPlayerToSpawnSkill).GetComponent<PlayerCharacter>().PlayerInventory;

        _skillsSpawner.SpawnSkill(_currentChoice[skillNumber],
                                  playerInventory.GetSkillLevel(_currentChoice[skillNumber]) + 1,
                                  _skillsSpawner.CurrentPlayerToSpawnSkill,
                                  out bool isMaxLevel);

        if (_skillsToOfferCount > 0)
            OfferNewSkillsView();
        else
        {
            _skillsOfferScreen.gameObject.SetActive(false);
            EventManager.OnActionMapSwitchEvent(ActionMaps.Player);
            Time.timeScale = 1;
        }
    }

    public void SkipSkillChoice()
    {
        _skillsOfferScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}