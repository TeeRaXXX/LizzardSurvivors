using UnityEngine;
using UnityEngine.Events;

public class NastyDollCollider : MonoBehaviour
{
    private UnityEvent<GameObject> _onTriggerEnterEvent;

    public void Initialize(UnityEvent<GameObject> onTriggerEnterEvent)
    {
        _onTriggerEnterEvent = onTriggerEnterEvent;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _onTriggerEnterEvent.Invoke(other.gameObject);
    }
}