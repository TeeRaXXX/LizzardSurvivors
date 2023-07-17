using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapGame : MonoBehaviour
{
    private void Awake()
    {
        GameDataStorage.Instance.Initialize();
        SceneManager.LoadScene("MainMenu");
    }
}