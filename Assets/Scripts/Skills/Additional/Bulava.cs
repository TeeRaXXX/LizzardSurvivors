using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bulava : MonoBehaviour
{
    [SerializeField] private NastyDollCollider _damageCollider;
    private UnityEvent<GameObject> OnTriggerEnterEvent;
    private List<string> _tagsToDamage;
    private float _damage;
    private bool isInited = false;

    private void Awake()
    {
        isInited = false;
        transform.localRotation = Quaternion.identity;
    }

    public void Initialize(List<string> tagsToDamage, float damage, float positionOffset)
    {
        transform.localPosition = new Vector3(positionOffset, 0, 0);
        _tagsToDamage = tagsToDamage;
        _damage = damage;
        OnTriggerEnterEvent = new UnityEvent<GameObject>();
        OnTriggerEnterEvent.AddListener(OnTriggerEnterObject);
        _damageCollider.Initialize(OnTriggerEnterEvent);
        isInited = true;
    }

    public void OnTriggerEnterObject(GameObject gameObject)
    {
        HealthComponent healthComponent = null;

        if (isInited && _tagsToDamage.Contains(gameObject.tag) && gameObject.TryGetComponent<HealthComponent>(out healthComponent))
        {
            healthComponent.ApplyDamage(_damage, gameObject);
        }
    }

    public void OnAnimationEnd()
    {
        OnTriggerEnterEvent.RemoveAllListeners();
        Destroy(gameObject);
    }
}