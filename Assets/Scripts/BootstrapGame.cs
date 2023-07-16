using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapGame : MonoBehaviour
{
    [SerializeField] private GameDataStorage _gameDataStorage;

    private void Awake()
    {
        _gameDataStorage.Initialize();
        SceneManager.LoadScene("Forest");
    }
}