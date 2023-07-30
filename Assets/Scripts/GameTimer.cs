using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    private float _localGameTime;
    private int _localGameSecond;
    private int _gameMinute;

    private bool _isTimerWorks;

    private GameTimer() { }

    public void Initialize()
    {
        Instance = this;
        _localGameTime = 0f;
        _localGameSecond = 0;
        _gameMinute = 0;
        _isTimerWorks = true;
    }

    private void Update()
    {
        if (_isTimerWorks)
        {
            _localGameTime += Time.deltaTime;

            if (_localGameTime >= 1f)
            {
                _localGameTime = 0f;
                _localGameSecond++;
                EventManager.OnNewGameSecondEvent(_localGameSecond);
            }

            if (_localGameSecond >= 60)
            {
                _gameMinute++;
                _localGameSecond = 0;
                _localGameTime = 0;
                Debug.Log($"New game minute - {_gameMinute}");
                EventManager.OnNewGameMinuteEvent(_gameMinute);
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