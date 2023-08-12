using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private GameObject _enemiesSpawnHandler;
    [SerializeField] private GameObject _gameTimer;
    [SerializeField] private GameObject _singleplayerUI;
    [SerializeField] private GameObject _multiplayerUI;
    [SerializeField] private GameObject _battleRoyaleUI;
    [SerializeField] private GameObject _destroyVolume;
    [SerializeField] private GameObject _skillsSpawner;
    [SerializeField] private GameObject _playerInput;
    [SerializeField] private GameObject _soundManager;
    [SerializeField] private GameObject _inputManager;
    [SerializeField] private SOCharacters _charactersList;
    private GameObject _eventSystem;

    public int StartLevel { get; private set; }
    public GameObject GetPlayerCharacterPrefab(CharacterType type) =>
        _charactersList.CharactersList.FirstOrDefault(c => c.CharacterType == type).CharacterPrefab;
    public GameObject GetEnemiesSpawnHandlerPrefab => _enemiesSpawnHandler;
    public GameObject GetGameTimerPrefab => _gameTimer;
    public GameObject GetSinglePlayerUIPrefab => _singleplayerUI;
    public GameObject GetMultiplayerUIPrefab => _multiplayerUI;
    public GameObject GetBattleRoyaleUIPrefab => _battleRoyaleUI;
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
#if UNITY_EDITOR
        var gameMode = gameModeBuilder.GetGameMode(EditorGodMode.Instance.GetGameModeChoice());
        List<CharacterType> charsList = EditorGodMode.Instance.GetCharactersChoice().FindAll(c => c != CharacterType.None);
#endif

        SpawnGameplayObject(_inputManager, Vector3.zero);
        InputManager.Instance.Initialize(_eventSystem, charsList.Count);
        gameMode.Initialize(charsList, this);
        EventManager.OnActionMapSwitchEvent(ActionMaps.Player);
    }

    public GameObject SpawnGameplayObject(GameObject gameObject, Vector3 spawnPosition)
    {
        return Instantiate(gameObject, spawnPosition, Quaternion.identity);
    }
}