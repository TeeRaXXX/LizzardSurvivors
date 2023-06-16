using UnityEngine;

public class BootstrapGame : MonoBehaviour
{
    [SerializeField] private GameDataStorage _gameDataStorage;
    [SerializeField] private GameTimer _gameTimer;

    private void Awake()
    {
        _gameDataStorage.Initialize();
        _gameTimer.Initialize();
    }
}