using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
public class EditorGodMode : EditorWindow
{
    public static EditorGodMode Instance;

    private int _characterIndex0;
    private int _characterIndex1;
    private int _characterIndex2;
    private int _characterIndex3;
    private static int _index = 0;
    private static bool _isPlaying = false;
    private static bool _charactersSaved = false;
    private static List<PlayerCharacter> _players;
    private static List<int> _playersIndexes;
    private static List<CharacterType> _characters;
    private static Dictionary<int, List<SkillType>> _skillsPlayers;
    private static SkillsSpawner _skillsSpawner;
    private static string[] _skillsList;

    [MenuItem("Nasty Doll/God Mode")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EditorGodMode));
    }

    private void OnEnable()
    {
        if (Instance == null)
            Instance = this;
    }

    private void InitializePlayers()
    {
        List<GameObject> playersPrefabs = GameObject.FindGameObjectsWithTag(TagsHandler.GetPlayerTag()).ToList();
        _players = new List<PlayerCharacter>();

        for (int i = 0; i < playersPrefabs.Count; i++)
        {
            _players.Add(playersPrefabs[i].GetComponent<PlayerCharacter>());
        }
    }

    public static void Initialize(SkillsSpawner skillsSpawner)
    {
        _skillsSpawner = skillsSpawner;
        _skillsPlayers = new Dictionary<int, List<SkillType>>();

        for (int i = 0; i < skillsSpawner.PlayersCount; i++)
        {
            _skillsPlayers.Add(i, new List<SkillType>());
        }

        EventManager.OnSkillAdded.AddListener(AddSkill);
        EventManager.OnSkillDeleted.AddListener(DeleteSkill);
        _skillsList = GetSkills();
    }

    private static void AddSkill(SkillType skill, int level, int playerIndex)
    {
        if (!_skillsPlayers[playerIndex].Contains(skill))
            _skillsPlayers[playerIndex].Add(skill);
    }

    private static void DeleteSkill(SkillType skill, int level, int playerIndex)
    {
        _skillsPlayers[playerIndex].Remove(skill);
    }

    public void OnGUI()
    {
        Repaint();
        if (!_isPlaying)
        {
            _characterIndex0 = EditorGUILayout.Popup($"Player 1", _characterIndex0, GetCharacters());
            _characterIndex1 = EditorGUILayout.Popup($"Player 2", _characterIndex1, GetCharacters());
            _characterIndex2 = EditorGUILayout.Popup($"Player 3", _characterIndex2, GetCharacters());
            _characterIndex3 = EditorGUILayout.Popup($"Player 4", _characterIndex3, GetCharacters());
        }

        if (_isPlaying)
        {
            if (_players == null)
            {
                InitializePlayers();
            }
            else
            {
                try
                {
                    _index = EditorGUILayout.Popup("Skill to add", _index, _skillsList);
                    EditorGUILayout.BeginHorizontal();

                    int playerOffset = 0;
                    foreach (var player in _players)
                    {
                        var skills = player.PlayerInventory.Skills;
                        Rect rectHorizontal = EditorGUILayout.BeginHorizontal();
                        rectHorizontal.height = 50;

                        if (GUI.Button(rectHorizontal, $"Add Skill to {player.Character.CharacterName}"))
                        {
                            _skillsSpawner.SpawnSkill((SkillType)_index, 1, player.PlayerIndex, out bool isMaxLevel);
                        }
                        int offsetY = 40;
                        foreach (var skill in _skillsPlayers[player.PlayerIndex])
                        {
                            Rect rectSkill = EditorGUILayout.BeginHorizontal();
                            rectSkill.width = rectHorizontal.width;
                            rectSkill.height = 40;
                            rectSkill.position = new Vector2(rectHorizontal.x, rectSkill.y + offsetY);

                            if (GUI.Button(rectSkill, $"{Enum.GetName(typeof(SkillType), skill)}"))
                            {
                                if (!player.PlayerInventory.IsSkillOnMaxLevel(skill))
                                {
                                    int skillLevel = player.PlayerInventory.GetSkillLevel(skill) + 1;
                                    _skillsSpawner.SpawnSkill(skill, skillLevel, player.PlayerIndex, out bool isMaxLevel);
                                }
                            }

                            GUI.Label(rectSkill, _skillsSpawner.GetSkillLogo(skill).texture);

                            GUIStyle style = new GUIStyle();
                            style.normal.textColor = Color.green;
                            style.alignment = TextAnchor.MiddleRight;
                            rectSkill.position = new Vector2(rectSkill.x - 40, rectSkill.y);

                            if (!player.PlayerInventory.IsSkillOnMaxLevel(skill))
                                GUI.Label(rectSkill, $"Level {player.PlayerInventory.GetSkillLevel(skill)}", style);
                            else GUI.Label(rectSkill, $"Max Level", style);

                            offsetY += 40;
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    playerOffset += 500;
                    EditorGUILayout.EndHorizontal();
                } catch { }
            }
        }
    }

    public List<CharacterType> GetCharactersChoice()
    {
        var characters = new List<CharacterType>();
        characters.Add((CharacterType)_characterIndex0);
        characters.Add((CharacterType)_characterIndex1);
        characters.Add((CharacterType)_characterIndex2);
        characters.Add((CharacterType)_characterIndex3);
        return characters;
    }

    private static string[] GetSkills()
    {
        string[] skills = new string[Enum.GetNames(typeof(SkillType)).Length - 1];

        for (int i = 0; i < skills.Length; i++)
        {
            if (Enum.GetNames(typeof(SkillType))[i] != SkillType.None.ToString() && !_skillsSpawner.IsEvolutionSkill((SkillType)i))
                skills[i] = Enum.GetNames(typeof(SkillType))[i];
        }

        return skills;
    }

    private static string[] GetCharacters()
    {
        string[] characters = new string[Enum.GetNames(typeof(CharacterType)).Length - 1];

        for (int i = 0; i < characters.Length; i++)
            characters[i] = Enum.GetNames(typeof(CharacterType))[i];

        return characters;
    }

    private EditorGodMode()
    {
        EditorApplication.playModeStateChanged += LogPlayModeState;
    }

    private void LogPlayModeState(PlayModeStateChange state)
    {
        switch (state) 
        {
            case PlayModeStateChange.EnteredPlayMode:
                _isPlaying = true;
                break;

            case PlayModeStateChange.ExitingPlayMode:
                _isPlaying = false;
                break;
        }  
    }
}
#endif