using NastyDoll.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Skill
{
    public SkillType SkillType;
    public GameObject SkillPrefab;
    public Skill(SkillType skillType, GameObject skillPrefab)
    {
        SkillType = skillType;
        SkillPrefab = skillPrefab;
    }
}

public class SkillsSpawner : MonoBehaviour
{
    [SerializeField] private SOSkills _skillsSO;

    private Dictionary<int, List<Skill>> _skills;
    private Queue<int> _playersIndexes;
    private int _playersCount;

    public void Initialize(int playersCount)
    {
        _skills = new Dictionary<int, List<Skill>>(playersCount);
        _playersIndexes = new Queue<int>(playersCount);

        for (int i = 0; i < playersCount; i++)
        {
            _skills.Add(i, new List<Skill>());
            _playersIndexes.Enqueue(i);
        }

        _playersCount = playersCount;
        EventManager.OnPlayerDied.AddListener(OnPlayerDied);
    }

    public int CurrentPlayerToSpawnSkill => _playersIndexes.Peek();
    public int PlayersCount { get { return _playersCount; } private set { } }

    public void SpawnSkill(SkillType skillType, int level, int playerIndex, out bool isMaxLevel)
    {
        Transform skillsHolder = GameObject.FindGameObjectsWithTag(TagsHandler.GetSkillsHolderTag()).
            FirstOrDefault(o => o.GetComponent<SkillsHolder>().PlayerIndex == playerIndex).GetComponent<Transform>();
        PlayerInventory playerInventory = GameObject.FindGameObjectsWithTag(TagsHandler.GetPlayerTag()).
            FirstOrDefault(o => o.GetComponent<PlayerCharacter>().PlayerIndex == playerIndex).GetComponent<PlayerCharacter>().PlayerInventory;
        isMaxLevel = false;

        if (!playerInventory.ContainsSkill(skillType))
        {
            GameObject skillPrefab = null;

            if (IsSkillActive(skillType))
            {
                skillPrefab = Instantiate(GetSkillPrefab(skillType), skillsHolder);
                _skills[playerIndex].Add(new Skill(skillType, skillPrefab));
                skillPrefab.GetComponent<IUpgradable>().Initialize(playerIndex);
            }
            else
            {
                List<int> players = new List<int>(_playersIndexes);
                for (int i = 0; i < _playersCount; i++)
                {
                    skillsHolder = GameObject.FindGameObjectsWithTag(TagsHandler.GetSkillsHolderTag()).
                        FirstOrDefault(o => o.GetComponent<SkillsHolder>().PlayerIndex == players[i]).GetComponent<Transform>();
                    skillPrefab = Instantiate(GetSkillPrefab(skillType), skillsHolder);
                    _skills[players[i]].Add(new Skill(skillType, skillPrefab));
                    skillPrefab.GetComponent<IUpgradable>().Initialize(players[i]);
                }
            }

            if (level == GetMaxLevelOfSkill(skillType))
            {
                isMaxLevel = true;
                return;
            }

            if (IsEvolutionSkill(skillType))
                skillPrefab.GetComponent<IEvolvedSkill>().Initialize(level);

            EventManager.OnSkillAddedEvent(skillType, level, playerIndex);

            UpdateCurrentPlayerToSpawnSkill();
        }
        else
        {
            if (playerInventory.GetSkillLevel(skillType) == GetMaxLevelOfSkill(skillType))
            {
                isMaxLevel = true;
                return;
            }

            if (IsSkillActive(skillType))
            {
                _skills[playerIndex].FirstOrDefault(skill => skill.SkillType == skillType).SkillPrefab.GetComponent<IUpgradable>().Upgrade(true);
                EventManager.OnSkillAddedEvent(skillType, level, playerIndex);
            }
            else
            {
                List<int> players = new List<int>(_playersIndexes);
                for (int i = 0; i < _playersCount; i++)
                {
                    _skills[players[i]].FirstOrDefault(skill => skill.SkillType == skillType).SkillPrefab.GetComponent<IUpgradable>().Upgrade(true);
                    EventManager.OnSkillAddedEvent(skillType, level, players[i]);
                }
            }

            UpdateCurrentPlayerToSpawnSkill();
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

    private void OnPlayerDied(int playerIndex)
    {
        _playersIndexes = new Queue<int>(_playersIndexes.Where(s => s != playerIndex));
        _skills.Remove(playerIndex);
        _playersCount--;
    }

    public void UpdateCurrentPlayerToSpawnSkill()
    {
        UtilsClass.MoveElementToBack(_playersIndexes, _playersIndexes.Peek());
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

    public List<SkillsEvolutionSO> GetSkillsEvolutionList()
    {
        return new List<SkillsEvolutionSO>(_skillsSO.SkillsEvolutionList);
    }

    public void DeleteSkill(SkillType skillToDelete, int playerIndex)
    {
        Destroy(_skills[playerIndex].FirstOrDefault(skill => skill.SkillType == skillToDelete).SkillPrefab);
        _skills[playerIndex].Remove(_skills[playerIndex].FirstOrDefault(skill => skill.SkillType == skillToDelete));
    }
}