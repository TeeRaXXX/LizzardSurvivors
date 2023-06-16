using UnityEngine;

public class InputManager : MonoBehaviour, IInitializeable
{
    private static Vector2 _playerMovement = new Vector2(0, 0);

    public static InputManager Instance;

    public void Initialize()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private InputManager() { }

    private void Update()
    {
        UpdatePlayerMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.OnEscapePressedEvent();
        }
    }

    private void UpdatePlayerMovement()
    {
        _playerMovement.x = Input.GetAxis("Horizontal");
        _playerMovement.y = Input.GetAxis("Vertical");
    }

    public Vector2 GetMovementVector() => _playerMovement;

}