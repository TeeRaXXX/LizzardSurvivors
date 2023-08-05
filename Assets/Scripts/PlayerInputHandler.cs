using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public enum ActionMaps
{
    Player,
    UI
}

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    public readonly UnityEvent<Vector2> OnMoveButtonPressed = new UnityEvent<Vector2>();
    public readonly UnityEvent OnMoveButtonReleased = new UnityEvent();
    public readonly UnityEvent OnBackButtonPressed = new UnityEvent();

    public void OnMoveButtonPressedEvent(Vector2 newMoveVector) => OnMoveButtonPressed.Invoke(newMoveVector);
    public void OnMoveButtonReleasedEvent() => OnMoveButtonReleased.Invoke();
    public void OnBackButtonPressedEvent() => OnBackButtonPressed.Invoke();

    private int _playerIndex;
    private static ActionMaps currentActionMap;

    public PlayerInput PlayerInput => _playerInput;

    private void Awake()
    {
        _playerIndex = _playerInput.playerIndex;
        _playerInput.uiInputModule = InputManager.Instance.UIInputModule;
        if (InputManager.Instance.PlayersCount == 1)
        {
            _playerInput.neverAutoSwitchControlSchemes = false;
        }

        _playerInput.actions["Move"].performed += MoveButtonPressed;
        _playerInput.actions["Move"].canceled += MoveButtonReleased;
        _playerInput.actions["Menu"].performed += MenuButtonPressed;

        _playerInput.actions["Cancel"].performed += BackButtonPressed;

        if (_playerIndex == 0)
        {
            _playerInput.actions.FindActionMap("Player").Enable();
            currentActionMap = ActionMaps.Player;
        }
        else _playerInput.actions.FindActionMap(currentActionMap.ToString()).Enable();

        EventManager.OnActionMapSwitch.AddListener(SwitchActionMapTo);

        FindObjectsOfType<PlayerCharacter>().FirstOrDefault(p => p.PlayerIndex == _playerIndex).SetPlayerInput(this);
    }

    public int GetPlayerIndex() => _playerIndex;

    ~PlayerInputHandler()
    {
        _playerInput.actions["Move"].performed -= MoveButtonPressed;
        _playerInput.actions["Move"].canceled -= MoveButtonReleased;

        _playerInput.actions["Cancel"].performed -= BackButtonPressed;
        _playerInput.actions["Menu"].performed -= MenuButtonPressed;
    }

    private void MoveButtonPressed(CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        OnMoveButtonPressedEvent(movement);
    }

    private void MoveButtonReleased(CallbackContext context)
    {
        OnMoveButtonPressedEvent(new Vector2(0f, 0f));
    }

    private void BackButtonPressed(CallbackContext context)
    {
        OnBackButtonPressedEvent();
    }

    private void MenuButtonPressed(CallbackContext context)
    {
        EventManager.OnPauseButtonPressedEvent();
    }

    private void SwitchActionMapTo(ActionMaps actionMap)
    {
        switch (actionMap)
        {
            case ActionMaps.Player:
                _playerInput.actions["Menu"].performed -= MenuButtonPressed;
                _playerInput.actions["Cancel"].performed -= BackButtonPressed;

                _playerInput.actions.FindActionMap("UI").Disable();
                _playerInput.actions.FindActionMap("Player").Enable();

                _playerInput.actions["Move"].performed += MoveButtonPressed;
                _playerInput.actions["Move"].canceled += MoveButtonReleased;
                _playerInput.actions["Menu"].performed += MenuButtonPressed;

                currentActionMap = ActionMaps.Player;
                Debug.Log("Action map set to Player");
                break;
            case ActionMaps.UI:
                OnMoveButtonPressedEvent(new Vector2(0f, 0f));

                _playerInput.actions["Move"].performed -= MoveButtonPressed;
                _playerInput.actions["Move"].canceled -= MoveButtonReleased;
                _playerInput.actions["Menu"].performed -= MenuButtonPressed;

                _playerInput.actions.FindActionMap("Player").Disable();
                _playerInput.actions.FindActionMap("UI").Enable();

                _playerInput.actions["Menu"].performed += MenuButtonPressed;
                _playerInput.actions["Cancel"].performed += BackButtonPressed;

                currentActionMap = ActionMaps.UI;

                Debug.Log("Action map set to UI");
                break;
        }
    }
}