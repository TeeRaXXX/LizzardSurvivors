using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    private UnityEvent _voidEvent;

    public UnityEvent GetVoidEvent()
    {
        _voidEvent = new UnityEvent();
        return _voidEvent;
    }

    public void OnVoidEvent()
    {
        if (_voidEvent != null)
            _voidEvent.Invoke();
    }
}