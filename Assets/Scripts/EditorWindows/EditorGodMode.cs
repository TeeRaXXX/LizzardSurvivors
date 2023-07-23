using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class EditorGodMode : EditorWindow
{
    private int index = 0;
    private static Dictionary<SkillType, int> _skillsLevels;
    private bool _isPlaying = false;
    private static SkillsSpawner _skillsSpawner;

    [MenuItem("Nasty Doll/God Mode")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EditorGodMode));
    }

    public static void Initialize(SkillsSpawner skillsSpawner)
    {
        _skillsSpawner = skillsSpawner;
        _skillsLevels = new Dictionary<SkillType, int>();
        EventManager.OnSkillAdded.AddListener(OnNewSkillAdded);
    }

    public void OnGUI()
    {
        if (_isPlaying)
        {
            index = EditorGUILayout.Popup("Skill to add", index, GetSkills());

            if (GUILayout.Button("Add Skill"))
            {
                _skillsSpawner.SpawnSkill((SkillType)index, out bool isMaxLevel);
            }

            GUILayout.Space(100);
            
            foreach (var skill in _skillsLevels)
            {
                if (GUILayout.Button($"{Enum.GetName(typeof(SkillType), skill.Key)} - Level {_skillsSpawner.GetCurrentSkillLevel(skill.Key)}"))
                {
                    if (!_skillsSpawner.IsSkillOnMaxLevel(skill.Key))
                        _skillsSpawner.SpawnSkill(skill.Key, out bool isMaxLevel);
                }
            }
        }
    }

    private static void OnNewSkillAdded(SkillType skill)
    {
        if (!_skillsLevels.ContainsKey(skill))
            _skillsLevels.Add(skill, 1);
    }

    private string[] GetSkills()
    {
        string[] skills = new string[Enum.GetNames(typeof(SkillType)).Length];

        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] = Enum.GetNames(typeof(SkillType))[i];
        }

        return skills;
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
                _skillsLevels.Clear();
                break;
        }  
    }
}