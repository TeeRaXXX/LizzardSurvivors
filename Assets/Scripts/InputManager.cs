using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager _playerInputManager;
    private List<PlayerInput> _playerInputs;
    private bool _isInited = false;

    public InputSystemUIInputModule UIInputModule { get; private set; }
    public EventSystem EventSystem { get; private set; }
    public int PlayersCount { get; private set; }
    public int ActivePlayers { get; private set; }
    public bool IsScreenSplitEnabled = false;
    public static InputManager Instance;

    public PlayerInputManager PlayerInputManager => _playerInputManager;

    public bool IsAllPlayersInited() => ActivePlayers == PlayersCount;

    public void EnableSinglePlayerInputControl(int playerIndex)
    {
        foreach (var playerInput in _playerInputs)
        {
            playerInput.DeactivateInput();
        }
        if (_playerInputs[playerIndex] != null)
        {
            _playerInputs[playerIndex].ActivateInput();
            UIInputModule.actionsAsset = _playerInputs[playerIndex].actions;
        }
    }

    public void EnableAllPlayerInputs()
    {
        foreach (var playerInput in _playerInputs)
        {
            playerInput.ActivateInput();
            UIInputModule.actionsAsset = new PlayerInputActions().asset;
        }
    }

    public void Initialize(GameObject eventSystem, int playersCount)
    {
        if (_isInited)
            Destroy(gameObject);

        _playerInputs = new List<PlayerInput>();
        IsScreenSplitEnabled = false;
        PlayersCount = playersCount;
        Instance = this;
        ActivePlayers = 0;
        UIInputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
        EventSystem = eventSystem.GetComponent<EventSystem>();
        _playerInputManager.onPlayerJoined += OnPlayerJoined;
        _isInited = true;
    }

    ~InputManager()
    {
        _playerInputManager.onPlayerJoined -= OnPlayerJoined;
    }

    public void EnableSplitScreen()
    {
        if (ActivePlayers == PlayersCount)
        {
            _playerInputManager.splitScreen = true;
        }
    }

    public void DisableSplitScreen() 
    {
        _playerInputManager.splitScreen = false;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"New Player was joined, index {playerInput.playerIndex}");
        _playerInputs.Add(playerInput);
        ActivePlayers++;
    }
}