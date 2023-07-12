using TMPro;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour, IInitializeable
{
    [SerializeField] private TMP_Text _gameTimer;
    [SerializeField] private TMP_Text _experienceCount;
    [SerializeField] private PlayerHealthBar _playerHealthBar;

    public void Initialize()
    {
        EventManager.OnNewGameSecond.AddListener(UpdateGameTimer);
        EventManager.OnExperienceUp.AddListener(UpdateExperienceCount);
        _playerHealthBar.Initialize();
        _experienceCount.text = PlayerLevel.Instance.Experience.ToString();
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

    private void UpdateExperienceCount(float experience)
    {
        _experienceCount.text = string.Format("{0:0}", PlayerLevel.Instance.Experience);
    }
}