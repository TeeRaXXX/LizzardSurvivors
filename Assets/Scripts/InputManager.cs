using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager _playerInputManager;
    public static InputSystemUIInputModule UIInputModule { get; private set; }

    private bool _isInited = false;
    public int PlayersCount { get; private set; }
    public int ActivePlayers { get; private set; }
    public bool IsScreenSplitEnabled = false;
    public static InputManager Instance;

    public PlayerInputManager PlayerInputManager => _playerInputManager;

    public bool IsAllPlayersInited() => ActivePlayers == PlayersCount;

    public void Initialize(InputSystemUIInputModule uiInputModule, int playersCount)
    {
        if (_isInited)
            Destroy(gameObject);

        IsScreenSplitEnabled = false;
        PlayersCount = playersCount;
        Instance = this;
        ActivePlayers = 0;
        UIInputModule = uiInputModule;
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
        ActivePlayers++;

        //if (PlayersCount == 1)
        //{
        //    _playerInputManager.DisableJoining();
        //}
    }
}