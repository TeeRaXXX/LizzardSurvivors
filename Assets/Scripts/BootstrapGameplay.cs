using System.Linq;
using UnityEngine;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private EnemiesSpawnHandler _enemiesSpawnHandler;
    [SerializeField] private GameTimer _gameTimer;
    [SerializeField] private GameplayUIManager _gameplayUIManager;
    [SerializeField] private SkillsSpawner _skillsSpawner;
    [SerializeField] private SOCharacters _charactersList;

    [SerializeField] private int _startLevel;

    private void Awake()
    {
        GameDataStorage.Instance.Initialize();

#if UNITY_EDITOR
        EditorGodMode.Initialize(_skillsSpawner);
#endif

        _gameTimer.Initialize();
        PlayerLevel.Instance.Initialize(_startLevel);
        _enemiesSpawnHandler.Initialize();
        _gameplayUIManager.Initialize(_skillsSpawner);
        _playerCharacter.Initialize(_skillsSpawner,
                                    _charactersList.CharactersList.FirstOrDefault(obj => obj.CharacterType == CharacterType.BabaYaga));
    }
}