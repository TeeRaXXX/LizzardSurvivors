using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private GameObject _playerCharacter;
    [SerializeField] private GameObject _enemiesSpawnHandler;
    [SerializeField] private GameObject _gameTimer;
    [SerializeField] private GameObject _singleplayerUI;
    [SerializeField] private GameObject _multiplayerUI;
    [SerializeField] private GameObject _skillsSpawner;
    [SerializeField] private GameObject _playerInput;
    [SerializeField] private GameObject _soundManager;
    [SerializeField] private GameObject _inputManager;
    [SerializeField] private SOCharacters _charactersList;
    [SerializeField] private InputSystemUIInputModule _uiInputModule;

    public int StartLevel { get; private set; }
    public GameObject GetPlayerCharacterPrefab => _playerCharacter;
    public GameObject GetEnemiesSpawnHandlerPrefab => _enemiesSpawnHandler;
    public GameObject GetGameTimerPrefab => _gameTimer;
    public GameObject GetSinglePlayerUIPrefab => _singleplayerUI;
    public GameObject GetMultiplayerUIPrefab => _multiplayerUI;
    public GameObject GetSkillsSpawnerPrefab => _skillsSpawner;
    public GameObject GetSoundManagerPrefab => _soundManager;
    public GameObject GetPlayerInputPrefab => _playerInput;
    public SOCharacters GetCharactersList => _charactersList;
    public InputSystemUIInputModule GetUIInputModule => _uiInputModule;

    private void Awake()
    {
        GameDataStorage.Instance.Initialize();

        StartLevel = 1;
        GameModeBuilder gameModeBuilder = new GameModeBuilder();
        var gameMode = gameModeBuilder.GetGameMode(GameModes.Survival);
        var charsList = new List<CharacterType> { CharacterType.BabaYaga, CharacterType.Druid};
        GameObject uiPrefab = charsList.Count > 1 ? _multiplayerUI : _singleplayerUI;
        gameMode.Initialize(charsList, uiPrefab, this);
        var inputManager = SpawnGameplayObject(_inputManager, Vector3.zero).GetComponent<InputManager>();
        inputManager.Initialize(_uiInputModule, charsList.Count);
        EventManager.OnActionMapSwitchEvent(ActionMaps.Player);
    }

    public GameObject SpawnGameplayObject(GameObject gameObject, Vector3 spawnPosition)
    {
        return Instantiate(gameObject, spawnPosition, Quaternion.identity);
    }
}