using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
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
#endif