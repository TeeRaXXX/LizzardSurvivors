using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void ShowSettingsWindow()
    {

    }

    public void ShowPlayWindow()
    {
        SceneManager.LoadScene("Forest");
    }

    public void Exit()
    {
        GameDataStorage.Instance.SaveData();
        Application.Quit();
    }
}