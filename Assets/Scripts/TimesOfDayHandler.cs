using NastyDoll.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable] public enum TimesOfDay
{
    Day,
    Night
}

[Serializable] public struct TimeOfDay
{
    public TimesOfDay TimeOfDayType;
    public Color TimeOfDayColor;
}


public class TimesOfDayHandler : MonoBehaviour
{
    [SerializeField] private List<TimeOfDay> _timesOfDayList;
    [SerializeField] private float _cycleTime;
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private float _timeOfDayChangeTime;

    public static TimesOfDayHandler Instance;

    private float _timeOfDayChangeCurrentTime;
    private bool _isReadyToChangeTimeOfDay;
    private TimeOfDay _currentTimeOfDay;
    private Queue<TimeOfDay> _timesOfDayQueue;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
        _isReadyToChangeTimeOfDay = false;
        _timeOfDayChangeCurrentTime = _timeOfDayChangeTime;
        _timesOfDayQueue = new Queue<TimeOfDay>(_timesOfDayList);
        _currentTimeOfDay = _timesOfDayQueue.Peek();
        _globalLight.color = _currentTimeOfDay.TimeOfDayColor;
        EventManager.OnTimerStarted.AddListener(Initialize);
    }

    private void Initialize()
    {
        StartCoroutine(Cycle());
        EventManager.OnTimerStarted.RemoveListener(Initialize);
    }

    private void Update()
    {
        if (_isReadyToChangeTimeOfDay)
            ChangeTimeOfDay();
    }

    private IEnumerator Cycle()
    {
        _isReadyToChangeTimeOfDay = false;

        yield return new WaitForSeconds(_cycleTime);

        UtilsClass.MoveElementToBack(_timesOfDayQueue, _timesOfDayQueue.Peek());
        _currentTimeOfDay = _timesOfDayQueue.Peek();
        EventManager.OnTimeOfDayChangedEvent(_currentTimeOfDay.TimeOfDayType);
        _isReadyToChangeTimeOfDay = true;
    }

    private void ChangeTimeOfDay()
    {
        if (_timeOfDayChangeCurrentTime <= Time.deltaTime)
        {
            _globalLight.color = _currentTimeOfDay.TimeOfDayColor;
            _timeOfDayChangeCurrentTime = _timeOfDayChangeTime;
            StartCoroutine(Cycle());
        }
        else
        {
            _globalLight.color = Color.Lerp(_globalLight.color, _currentTimeOfDay.TimeOfDayColor, Time.deltaTime / _timeOfDayChangeCurrentTime);

            _timeOfDayChangeCurrentTime -= Time.deltaTime;
        }
    }
}