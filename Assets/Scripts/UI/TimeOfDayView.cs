using UnityEngine;

public class TimeOfDayView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake() => EventManager.OnTimeOfDayChanged.AddListener(ChangeState);

    private void ChangeState(TimesOfDay timeOfDay)
    {
        switch (timeOfDay)
        {
            case TimesOfDay.Day:
                _animator.SetBool("NightBegin", false);
                _animator.SetBool("DayBegin", true);
                break;

            case TimesOfDay.Night:
                _animator.SetBool("DayBegin", false);
                _animator.SetBool("NightBegin", true);
                break;
        }
    }
}