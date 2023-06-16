using UnityEngine;

public class BootstrapMainGame : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    private void Awake()
    {
        _inputManager.Initialize();
    }
}