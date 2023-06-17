using TMPro;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour, IInitializeable
{
    [SerializeField] private TMP_Text _gameTimer;
    [SerializeField] private PlayerHealthBar _playerHealthBar;

    public void Initialize()
    {
        EventManager.OnNewGameSecond.AddListener(UpdateGameTimer);
        _playerHealthBar.Initialize();
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
}
