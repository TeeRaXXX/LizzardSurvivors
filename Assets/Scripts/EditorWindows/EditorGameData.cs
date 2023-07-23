using UnityEditor;
using UnityEngine;

public class EditorGameData : EditorWindow 
{
    [MenuItem("Nasty Doll/Game Data")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EditorGameData));
    }

    void OnGUI()
    {
        if (GUILayout.Button("Reset Game Data"))
        {
            GameDataStorage.Instance.ResetData();
        }
    }
}