using NastyDoll.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable] public struct BattleroyaleSkillView
{
    public GameObject SkillsViewObject;
    public List<SkillView> ActiveSkills;
    public List<SkillView> PassiveSkills;
}


public class BattleroyaleSkillsView : MonoBehaviour
{
    [SerializeField] private List<BattleroyaleSkillView> BattleroyaleSkillViews;
    [SerializeField] Sprite _skillSpritePlaceholder;
    [SerializeField] Image _characterLogoSkillSelect;

    private SkillsSpawner _skillsSpawner;
    private int _currentPlayersCount;

    public void Initialize(SkillsSpawner skillsSpawner)
    {
        _skillsSpawner = skillsSpawner;
        ResetAllSkills(-1);

        EventManager.OnSkillAdded.AddListener(AddSkill);
        EventManager.OnSkillDeleted.AddListener(DeleteSkill);
        EventManager.OnPlayerInitialized.AddListener(InitializePlayer);
        EventManager.OnPlayerDied.AddListener(ResetAllSkills);
    }

    private void InitializePlayer(PlayerCharacter playerCharacter)
    {
        BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerCharacter.PlayerIndex].CharacterLogo.sprite = playerCharacter.PlayerLogo;
    }

    private void AddSkill(SkillType skillType, int skillLevel, int playerIndex)
    {
        int index = 0;
        
        if (_skillsSpawner.IsSkillActive(skillType) && !BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerIndex].SkillTypes.Contains(skillType))
        {
            for (int i = 0; i < BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerIndex].SkillTypes.Count; i++)
            {
                if (BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerIndex].SkillSprites[i].sprite == _skillSpritePlaceholder)
                {
                    BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerIndex].SkillSprites[i].sprite = _skillsSpawner.GetSkillLogo(skillType);
                    BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerIndex].SkillTypes[i] = skillType;
                    break;
                }

                index++;
            }
        }
        else if (!_skillsSpawner.IsSkillActive(skillType) && !BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[playerIndex].SkillTypes.Contains(skillType))
        {
            for (int i = 0; i < BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[playerIndex].SkillTypes.Count; i++)
            {
                if (BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[playerIndex].SkillSprites[i].sprite == _skillSpritePlaceholder)
                {
                    BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[playerIndex].SkillSprites[i].sprite = _skillsSpawner.GetSkillLogo(skillType);
                    BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[playerIndex].SkillTypes[i] = skillType;
                    break;
                }

                index++;
            }
        }
    }

    private void DeleteSkill(SkillType skillType, int skillLevel, int playerIndex)
    {
        int index = 0;

        if (_skillsSpawner.IsSkillActive(skillType) && BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerIndex].SkillTypes.Contains(skillType))
        {
            for (int i = 0; i < BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerIndex].SkillTypes.Count; i++)
            {
                if (BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerIndex].SkillSprites[i].sprite == _skillsSpawner.GetSkillLogo(skillType))
                {
                    BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerIndex].SkillSprites[i].sprite = _skillSpritePlaceholder;
                    BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[playerIndex].SkillTypes[i] = SkillType.None;
                    break;
                }

                index++;
            }
        }
        else if (!_skillsSpawner.IsSkillActive(skillType) && BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[playerIndex].SkillTypes.Contains(skillType))
        {
            for (int i = 0; i < BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[playerIndex].SkillTypes.Count; i++)
            {
                if (BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[playerIndex].SkillSprites[i].sprite == _skillsSpawner.GetSkillLogo(skillType))
                {
                    BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[playerIndex].SkillSprites[i].sprite = _skillSpritePlaceholder;
                    BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[playerIndex].SkillTypes[i] = SkillType.None;
                    break;
                }

                index++;
            }
        }
    }

    private void ResetAllSkills(int diedPlayerIndex = -1)
    {
        if (diedPlayerIndex == -1)
            _currentPlayersCount = _skillsSpawner.PlayersCount;
        else _currentPlayersCount--;

        for (int i = 0; i < BattleroyaleSkillViews.Count; i++)
            BattleroyaleSkillViews[i].SkillsViewObject.SetActive(false);

        switch (_currentPlayersCount)
        {
            case 2:
                BattleroyaleSkillViews[0].SkillsViewObject.SetActive(true);
                break;
            case 3:
                BattleroyaleSkillViews[1].SkillsViewObject.SetActive(true);
                break;
            case 4:
                BattleroyaleSkillViews[2].SkillsViewObject.SetActive(true);
                break;
        }

        for (int i = 0; i < _currentPlayersCount; i++)
        {
            for (int j = 0; j < BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[i].SkillTypes.Count; j++)
            {
                BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[i].SkillSprites[j].sprite = _skillSpritePlaceholder;
                BattleroyaleSkillViews[_currentPlayersCount - 2].ActiveSkills[i].SkillTypes[j] = SkillType.None;
                BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[i].SkillSprites[j].sprite = _skillSpritePlaceholder;
                BattleroyaleSkillViews[_currentPlayersCount - 2].PassiveSkills[i].SkillTypes[j] = SkillType.None;
            }
        }

        if (diedPlayerIndex != -1)
        {
            var playersPrefabs = UtilsClass.FindObjectsWithTagsList(TagsHandler.GetPlayerTags());
            List<PlayerCharacter> players = new List<PlayerCharacter>();

            foreach (var playerPrefab in playersPrefabs)
            {
                players.Add(playerPrefab.GetComponent<PlayerCharacter>());
            }

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].PlayerIndex == diedPlayerIndex)
                    continue;

                InitializePlayer(players[i]);
                foreach (var skill in players[i].PlayerInventory.Skills)
                {
                    AddSkill(skill.Key, 1, i);
                }
            }
        }
    }
}