using UnityEngine;

public class GameTimer : MonoBehaviour, IInitializeable
{
    public static GameTimer Instance;

    private float _gameTime;
    private int _gameTimeInSeconds;
    private bool _isTimerWorks;

    private GameTimer() { }

    public void Initialize()
    {
        Instance = this;
        _gameTime = 0f;
        _gameTimeInSeconds = 0;
        _isTimerWorks = true;
    }

    private void Update()
    {
        if (_isTimerWorks)
        {
            _gameTime += Time.deltaTime;

            if ((int)(_gameTime % 100f) > _gameTimeInSeconds)
            {
                _gameTimeInSeconds++;
                EventManager.OnNewGameSecondEvent(_gameTimeInSeconds);
            }

            if ((int)(_gameTime % 6000f) > _gameTimeInSeconds)
            {
                EventManager.OnNewGameMinuteEvent(_gameTimeInSeconds / 6000);
            }
        }
    }

    public float GetGameTime() => _gameTime;

    public void ResetGameTimer()
    {
        _isTimerWorks = false;
        _gameTime = 0f;
        _isTimerWorks = true;
    }
}