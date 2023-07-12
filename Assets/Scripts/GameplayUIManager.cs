using TMPro;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour, IInitializeable
{
    [SerializeField] private TMP_Text _gameTimer;
    [SerializeField] private TMP_Text _playerLevel;
    [SerializeField] private PlayerHealthView _playerHealthBar;
    [SerializeField] private PlayerExperienceView _playerExperienceBar;

    public void Initialize()
    {
        EventManager.OnNewGameSecond.AddListener(UpdateGameTimer);
        EventManager.OnLevelUp.AddListener(UpdatePlayerLevel);
        _playerHealthBar.Initialize();
        _playerExperienceBar.Initialize();
        _playerLevel.text = PlayerLevel.Instance.Level.ToString();
    }

    private void UpdateGameTimer(int gameSecond)
    {
        int minutes = gameSecond / 60;
        int seconds = gameSecond % 60;
        string minutesString;
        string secondsString;

        if (minutes > 99)
            minutesString = string.Format("{0:000}", minutes);
        else minutesString = string.Format("{0:00}", minutes);

        secondsString = string.Format("{0:00}", seconds);

        _gameTimer.text = $"{minutesString}:{secondsString}";
    }

    private void UpdatePlayerLevel(int level)
    {
        _playerLevel.text = level.ToString();
    }
}