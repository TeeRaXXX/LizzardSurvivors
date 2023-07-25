using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour
{
    public void Initialize()
    {
        EventManager.OnGameOver.AddListener(ShowWindow);
        gameObject.SetActive(false);
    }

    private void ShowWindow()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void ToMainMenu()
    {
        GameDataStorage.Instance.SaveData();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}