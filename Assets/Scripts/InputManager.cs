using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static readonly UnityEvent<Vector2> OnMoveButtonPressed = new UnityEvent<Vector2>();
    public static readonly UnityEvent OnMoveButtonReleased = new UnityEvent();
    public static readonly UnityEvent OnBackButtonPressed = new UnityEvent();
    public static readonly UnityEvent OnMenuButtonPressed = new UnityEvent();

    public static void OnMovebuttonPressedEvent(Vector2 newMoveVector) => OnMoveButtonPressed.Invoke(newMoveVector);
    public static void OnMoveButtonReleasedEvent() => OnMoveButtonReleased.Invoke();
    public static void OnBackButtonPressedEvent() => OnBackButtonPressed.Invoke();
    public static void OnMenuButtonPressedEvent() => OnMenuButtonPressed.Invoke();

    private PlayerInputActions _inputActions;

    public void Initialize()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
        _inputActions.UI.Enable();

        _inputActions.Player.Move.performed += MoveButtonPressed;
        _inputActions.Player.Move.canceled += MoveButtonReleased;

        _inputActions.UI.Cancel.performed += BackButtonPressed;
        _inputActions.UI.Menu.performed += MenuButtonPressed;
    }

    ~InputManager()
    {
        _inputActions.Player.Move.performed -= MoveButtonPressed;
        _inputActions.Player.Move.canceled -= MoveButtonReleased;

        _inputActions.UI.Cancel.performed -= BackButtonPressed;
        _inputActions.UI.Menu.performed -= MenuButtonPressed;
    }

    private void MoveButtonPressed(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        OnMovebuttonPressedEvent(movement);
    }

    private void MoveButtonReleased(InputAction.CallbackContext context)
    {
        OnMovebuttonPressedEvent(new Vector2(0f, 0f));
    }

    private void BackButtonPressed(InputAction.CallbackContext context)
    {
        OnBackButtonPressedEvent();
    }

    private void MenuButtonPressed(InputAction.CallbackContext context)
    {
        OnMenuButtonPressedEvent();
    }
}