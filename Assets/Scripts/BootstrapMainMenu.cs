using UnityEngine;

public class BootstrapMainMenu : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private StudioLogoView _studioLogoView;

    private void Awake()
    {
        GameDataStorage.Instance.Initialize();
        _studioLogoView.Initialize();
    }
}