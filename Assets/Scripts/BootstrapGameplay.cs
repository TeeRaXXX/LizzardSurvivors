using UnityEngine;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private EnemiesSpawnHandler _enemiesSpawnHandler;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameTimer _gameTimer;
    [SerializeField] private GameplayUIManager _gameplayUIManager;
    [SerializeField] private SkillsSpawner _skillsSpawner;

    [SerializeField] private int _startLevel;

    private void Awake()
    {
        _gameTimer.Initialize();
        _inputManager.Initialize();
        PlayerLevel.Initialize(_startLevel);
        _enemiesSpawnHandler.Initialize();
        _gameplayUIManager.Initialize(_skillsSpawner);
        _playerCharacter.Initialize(_skillsSpawner);
    }
}