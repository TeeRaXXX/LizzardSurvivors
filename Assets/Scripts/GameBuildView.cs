using TMPro;
using UnityEngine;

public class GameBuildView : MonoBehaviour
{
    [SerializeField] private string _gameBuild;
    [SerializeField] private TMP_Text _textView;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _textView.text = _gameBuild;
    }
}