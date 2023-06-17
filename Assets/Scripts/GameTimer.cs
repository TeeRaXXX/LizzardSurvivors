using UnityEngine;

public class GameTimer : MonoBehaviour, IInitializeable
{
    public static GameTimer Instance;

    private int _gameTimeInSeconds;
    private bool _isTimerWorks;

    private float _localGameTime;
    private int _localGameSecond;

    private GameTimer() { }

    public void Initialize()
    {
        Instance = this;
        _localGameTime = 0f;
        _localGameSecond = 0;
        _gameTimeInSeconds = 0;
        _isTimerWorks = true;
    }

    private void Update()
    {
        if (_isTimerWorks)
        {
            _localGameTime += Time.deltaTime;

            if (_localGameTime > 0.99f)
            {
                _localGameTime = 0f;
                _gameTimeInSeconds++;
                _localGameSecond++;
                EventManager.OnNewGameSecondEvent(_gameTimeInSeconds);
            }

            if (_localGameSecond >= 60)
            {
                EventManager.OnNewGameMinuteEvent(_gameTimeInSeconds / 60);
            }

            if (_localGameSecond >= 60)
            {
                _localGameSecond = 0;
            }
        }
    }

    public void ResetGameTimer()
    {
        _isTimerWorks = false;
        _localGameTime = 0f;
        _isTimerWorks = true;
    }
}