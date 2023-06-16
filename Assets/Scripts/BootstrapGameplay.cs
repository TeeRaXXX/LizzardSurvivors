using UnityEngine;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private EnemiesSpawnHandler _enemiesSpawnHandler;
    [SerializeField] private EnemiesDeathHandler _enemiesDeathHandler;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameTimer _gameTimer;

    private void Awake()
    {
        _gameTimer.Initialize();
        _inputManager.Initialize();
        _playerCharacter.Initialize();
        _enemiesSpawnHandler.Initialize();
        _enemiesDeathHandler.Initialize();
    }
}