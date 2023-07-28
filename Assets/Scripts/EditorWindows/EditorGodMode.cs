using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class EditorGodMode : EditorWindow
{
    private int index = 0;
    private static List<SkillType> _skills;
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
        _skills = new List<SkillType>();
        EventManager.OnSkillAdded.AddListener(OnNewSkillAdded);
        EventManager.OnSkillDeleted.AddListener(OnSkillDelete);
    }

    public void OnGUI()
    {
        if (_isPlaying)
        {
            index = EditorGUILayout.Popup("Skill to add", index, GetSkills());

            if (GUILayout.Button("Add Skill"))
            {
                _skillsSpawner.SpawnSkill((SkillType)index, 1, out bool isMaxLevel);
            }
            
            foreach (var skill in _skills)
            {
                Rect rect = EditorGUILayout.BeginHorizontal();

                if (GUI.Button(rect, $"{Enum.GetName(typeof(SkillType), skill)}"))
                {
                    if (!_skillsSpawner.IsSkillOnMaxLevel(skill))
                    {
                        int skillLevel = _skillsSpawner.GetCurrentSkillLevel(skill) + 1;
                        _skillsSpawner.SpawnSkill(skill, skillLevel, out bool isMaxLevel);
                    }
                        
                }

                GUILayout.Label(_skillsSpawner.GetSkillLogo(skill).texture);

                rect.position = new Vector2(rect.x - 30, rect.y);
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.green;
                style.alignment = TextAnchor.MiddleRight;

                if (!_skillsSpawner.IsSkillOnMaxLevel(skill))
                    GUI.Label(rect, $"Level {_skillsSpawner.GetCurrentSkillLevel(skill)}", style);
                else GUI.Label(rect, $"Max Level", style);

                EditorGUILayout.EndHorizontal();
            }
            Repaint();
        }
    }

    private static void OnNewSkillAdded(SkillType skill, int level)
    {
        if (!_skills.Contains(skill))
            _skills.Add(skill);
    }

    private static void OnSkillDelete(SkillType skill, int level)
    {
        _skills.Remove(skill);
    }

    private string[] GetSkills()
    {
        string[] skills = new string[Enum.GetNames(typeof(SkillType)).Length - 1];

        for (int i = 0; i < skills.Length; i++)
        {
            if (Enum.GetNames(typeof(SkillType))[i] != SkillType.None.ToString() && !_skillsSpawner.IsEvolutionSkill((SkillType)i))
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
                _skills.Clear();
                break;
        }  
    }
}