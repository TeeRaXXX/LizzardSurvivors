using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private GameObject _playerCharacter;
    [SerializeField] private GameObject _enemiesSpawnHandler;
    [SerializeField] private GameObject _gameTimer;
    [SerializeField] private GameObject _singleplayerUI;
    [SerializeField] private GameObject _multiplayerUI;
    [SerializeField] private GameObject _destroyVolume;
    [SerializeField] private GameObject _skillsSpawner;
    [SerializeField] private GameObject _playerInput;
    [SerializeField] private GameObject _soundManager;
    [SerializeField] private GameObject _inputManager;
    [SerializeField] private SOCharacters _charactersList;
    private GameObject _eventSystem;

    public int StartLevel { get; private set; }
    public GameObject GetPlayerCharacterPrefab => _playerCharacter;
    public GameObject GetEnemiesSpawnHandlerPrefab => _enemiesSpawnHandler;
    public GameObject GetGameTimerPrefab => _gameTimer;
    public GameObject GetSinglePlayerUIPrefab => _singleplayerUI;
    public GameObject GetMultiplayerUIPrefab => _multiplayerUI;
    public GameObject GetSkillsSpawnerPrefab => _skillsSpawner;
    public GameObject GetDestroyVolumePrefab => _destroyVolume;
    public GameObject GetSoundManagerPrefab => _soundManager;
    public GameObject GetPlayerInputPrefab => _playerInput;
    public GameObject GetUIInputModule => _eventSystem;
    public SOCharacters GetCharactersList => _charactersList;

    private void Awake()
    {
        GameDataStorage.Instance.Initialize();
        _eventSystem = GameObject.FindGameObjectWithTag(TagsHandler.GetEventSystemTagTag());

        StartLevel = 1;
        GameModeBuilder gameModeBuilder = new GameModeBuilder();
        var gameMode = gameModeBuilder.GetGameMode(GameModes.Survival);

#if UNITY_EDITOR
        List<CharacterType> charsList = EditorGodMode.Instance.GetCharactersChoice().FindAll(c => c != CharacterType.None);
#endif

        SpawnGameplayObject(_inputManager, Vector3.zero);
        InputManager.Instance.Initialize(_eventSystem, charsList.Count);
        GameObject uiPrefab = charsList.Count > 1 ? _multiplayerUI : _singleplayerUI;
        gameMode.Initialize(charsList, uiPrefab, this);
        EventManager.OnActionMapSwitchEvent(ActionMaps.Player);
    }

    public GameObject SpawnGameplayObject(GameObject gameObject, Vector3 spawnPosition)
    {
        return Instantiate(gameObject, spawnPosition, Quaternion.identity);
    }
}