using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseView : MonoBehaviour
{
    [SerializeField] private GameObject _gamePauseUI;

    private bool _paused;

    public void Initialize()
    {
        _gamePauseUI.SetActive(false);
        EventManager.OnPauseButtonPressed.AddListener(Pause);
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
                EventManager.OnActionMapSwitchEvent(ActionMaps.UI);
            }
            else if (_gamePauseUI.activeSelf)
            {
                _paused = false;
                Time.timeScale = 1f;
                _gamePauseUI.SetActive(false);
                EventManager.OnActionMapSwitchEvent(ActionMaps.Player);
            }
        }    
    }

    public void OnQuitPressed()
    {
        Time.timeScale = 1f;
        GameDataStorage.Instance.SaveData();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPlayPressed()
    {
        Time.timeScale = 1f;
        _gamePauseUI.SetActive(false);
    }
}