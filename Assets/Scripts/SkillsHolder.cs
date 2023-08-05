using UnityEngine;

public class SkillsHolder : MonoBehaviour
{
    private int _playerIndex;

    public int PlayerIndex { get { return _playerIndex; } }

    public void Initialize(int playerIndex)
    {
        _playerIndex = playerIndex;
    }
}