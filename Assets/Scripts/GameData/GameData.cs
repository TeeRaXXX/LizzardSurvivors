using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct CharacterData
{
    public CharacterType Type;
    public bool IsOpened;
    public bool IsBought;
}

[Serializable] public struct EnemiesData
{
    public EnemyType Type;
    public bool IsOpened;
}

[Serializable] public struct SkillsData
{
    public SkillType Type;
    public bool IsOpened;
    public bool IsActive;
}

[Serializable] public class GameData
{
    [SerializeField] private List<CharacterData> _discoveredCharacters;
    [SerializeField] private List<EnemiesData> _discoveredEnemies;
    [SerializeField] private List<SkillsData> _discoveredSkills;
    [SerializeField] private List<Achievement> _achievements;
    [SerializeField] private List<MatchData> _playedMatches;
    [SerializeField] private PlayerStats _permanentPlayerStats;
    [SerializeField] private int _coinsCount;
    [SerializeField] private float _audioVolume;

    public GameData()
    {
        _coinsCount = 0;
        _audioVolume = 1f;

        CharacterData baseCharacter = new CharacterData();
        baseCharacter.Type = CharacterType.Monk;
        baseCharacter.IsOpened = true;
        baseCharacter.IsBought = true;
        _discoveredCharacters = new List<CharacterData>();
        _discoveredSkills = new List<SkillsData>();
        _discoveredCharacters.Add(baseCharacter);
    }

    public void AddCoins(int coinsCount)
    {
        if (coinsCount > 0)
            _coinsCount += coinsCount;
    }

    public void RemoveCoins(int coinsCount)
    {
        if (coinsCount < 0)
            _coinsCount -= coinsCount;
    }

    public void AddOpenedCharacter(CharacterType characterType)
    {
        CharacterData newCharacter = new CharacterData();
        newCharacter.Type = characterType;
        newCharacter.IsOpened = true;
        newCharacter.IsBought = false;
        _discoveredCharacters.Add(newCharacter);
    }

    public void BuyCharacter(CharacterType characterType)
    {
        for (int i = 0; i < _discoveredCharacters.Count; i++)
        {
            if (_discoveredCharacters[i].Type == characterType)
            {
                CharacterData newCharacter = new CharacterData();
                newCharacter.Type = characterType;
                newCharacter.IsOpened = true;
                newCharacter.IsBought = true;
                _discoveredCharacters[i] = newCharacter;
            }
        }
    }
    public void AddOpenedEnemy(EnemyType enemyType)
    {
        EnemiesData newEnemy = new EnemiesData();
        newEnemy.Type = enemyType;
        newEnemy.IsOpened = true;
        _discoveredEnemies.Add(newEnemy);
    }

    public void AddOpenedSkill(SkillType skillType, bool isSkillActive)
    {
        SkillsData newSkill = new SkillsData();
        newSkill.Type = skillType;
        newSkill.IsOpened = true;
        newSkill.IsActive = isSkillActive;
        _discoveredSkills.Add(newSkill);
    }

    public List<MatchData> GetMatches()
    {
        return _playedMatches;
    }

    public PlayerStats GetPermanentPlayerStats()
    {
        return _permanentPlayerStats;
    }

    public List<SkillType> GetDiscoveredSkillsInstead(List<SkillType> instead = null)
    {
        var skills = new List<SkillType>();

        if (instead == null)
        {
            foreach (var skill in _discoveredSkills)
                if (skill.IsOpened)
                    skills.Add(skill.Type);
        }
        else
        {
            foreach (var skill in _discoveredSkills)
                if (skill.IsOpened && !instead.Exists(type => type == skill.Type))
                {
                    skills.Add(skill.Type);
                }
        }

        return skills;
    }
}