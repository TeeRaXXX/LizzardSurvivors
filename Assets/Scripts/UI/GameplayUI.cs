using System;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _gameTimer;
    [SerializeField] private TMP_Text _playerLevel;
    [SerializeField] private PlayerExperienceView _playerExperienceBar;
    [SerializeField] private PlayerSkillsView _playerSkillsView;
    [SerializeField] private BattleroyaleSkillsView _battleroyaleSkillsView;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private GamePauseView _gamePauseView;

    private int _gameMinute;

    public void Initialize(SkillsSpawner skillsHandler, int playersCount, GameModes gameMode)
    {
        switch (gameMode)
        {
            case GameModes.Survival:
                EventManager.OnNewGameSecond.AddListener(UpdateSecondsTimer);
                EventManager.OnLevelUp.AddListener(UpdatePlayerLevel);
                _playerExperienceBar.Initialize();
                _playerSkillsView.Initialize(skillsHandler);
                _gameOverView.Initialize();
                _gamePauseView.Initialize();
                _playerLevel.text = PlayerLevel.Instance.Level.ToString();
                break;

            case GameModes.Ancients:
                break;

            case GameModes.Battleroyale:
                EventManager.OnNewGameSecond.AddListener(UpdateSecondsTimer);
                _battleroyaleSkillsView.Initialize(skillsHandler);
                _gameOverView.Initialize();
                _gamePauseView.Initialize();
                break;
        }
    }

    private void UpdateSecondsTimer(int gameSecond)
    {
        if (gameSecond == 60)
        {
            gameSecond = 0;
            _gameMinute++;
        }

        _gameTimer.text = string.Format("{0:00}:{1:00}", _gameMinute, gameSecond);
    }

    private void UpdatePlayerLevel(int level)
    {
        _playerLevel.text = (Int32.Parse(_playerLevel.text) + level).ToString();
    }
}