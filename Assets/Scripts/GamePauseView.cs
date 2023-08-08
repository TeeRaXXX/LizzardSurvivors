using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseView : MonoBehaviour
{
    [SerializeField] private GameObject _gamePauseUI;
    [SerializeField] private GameObject _playButton;

    private bool _paused;

    public void Initialize()
    {
        _gamePauseUI.SetActive(false);
        EventManager.OnPauseButtonPressed.AddListener(Pause);
        InputManager.Instance.EventSystem.SetSelectedGameObject(_playButton);
    }

    private void Pause()
    {
        if (Time.timeScale != 0f || _paused)
        {
            if (!_gamePauseUI.activeSelf)
            {
                _paused = true;
                Time.timeScale = 0f;
                _gamePauseUI.SetActive(true);
                InputManager.Instance.EventSystem.SetSelectedGameObject(_playButton);
                InputManager.Instance.EnableAllPlayerInputs();
                EventManager.OnActionMapSwitchEvent(ActionMaps.UI);
            }
            else if (_gamePauseUI.activeSelf)
            {
                _paused = false;
                EventManager.OnActionMapSwitchEvent(ActionMaps.Player);
                Time.timeScale = 1f;
                _gamePauseUI.SetActive(false);
            }
        }    
    }

    public void OnQuitPressed()
    {
        EventManager.OnActionMapSwitchEvent(ActionMaps.UI);
        Time.timeScale = 1f;
        GameDataStorage.Instance.SaveData();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPlayPressed()
    {
        EventManager.OnActionMapSwitchEvent(ActionMaps.Player);
        Time.timeScale = 1f;
        _gamePauseUI.SetActive(false);
    }
}