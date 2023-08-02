using System.Collections.Generic;
using UnityEngine;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private GameObject _playerCharacter;
    [SerializeField] private GameObject _enemiesSpawnHandler;
    [SerializeField] private GameObject _gameTimer;
    [SerializeField] private GameObject _gameplayUIManager;
    [SerializeField] private GameObject _skillsSpawner;
    [SerializeField] private SOCharacters _charactersList;
    public int StartLevel { get; private set; }
    public GameObject GetPlayerCharacterPrefab => _playerCharacter;
    public GameObject GetEnemiesSpawnHandlerPrefab => _enemiesSpawnHandler;
    public GameObject GetGameTimerPrefab => _gameTimer;
    public GameObject GetGameplayUIManagerPrefab => _gameplayUIManager;
    public GameObject GetSkillsSpawnerPrefab => _skillsSpawner;
    public SOCharacters GetCharactersList => _charactersList;

    private void Awake()
    {
        GameDataStorage.Instance.Initialize();

        StartLevel = 1;
        GameModeBuilder gameModeBuilder = new GameModeBuilder();
        var gameMode = gameModeBuilder.GetGameMode(GameModes.Survival);
        gameMode.Initialize(new List<CharacterType> { CharacterType.BabaYaga }, this);
    }

    public GameObject SpawnGameplayObject(GameObject gameObject, Vector3 spawnPosition)
    {
        return Instantiate(gameObject, spawnPosition, Quaternion.identity);
    }
}