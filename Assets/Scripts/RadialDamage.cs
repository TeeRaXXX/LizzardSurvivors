using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialDamage : MonoBehaviour
{
    private List<string> _tagsToDamage;
    private float _damage = 5f;
    private float _damageFrequency = 1f;
    private float _damageRadius;

    [SerializeField] private List<Collider2D> _objectsToDamage;
    private bool _isWorking = false;
    private bool _isEnable = false;

    private void Awake()
    {
        _isWorking = false;
        _isEnable = false;
        _objectsToDamage = new List<Collider2D>();
    }

    public void Initialize(List<string> tagsToDamage, float damageRadius, float damage, float damageFrequency, float lifeTime = -1f)
    {
        UpdateStats(tagsToDamage, damageRadius, damage, damageFrequency);
        _isEnable = true;

        if (lifeTime > 0f)
            StartCoroutine(LifeTime(lifeTime));
    }

    public void UpdateStats(List<string> tagsToDamage, float damageRadius, float damage, float damageFrequency)
    {
        _tagsToDamage = new List<string>(tagsToDamage);
        _damage = damage;
        _damageFrequency = damageFrequency;
        _damageRadius = damageRadius;

        transform.localScale = new Vector3(_damageRadius, _damageRadius, 1f);
    }

    private void FixedUpdate()
    {
        if (_objectsToDamage.Count > 0 && !_isWorking && _isEnable)
        {
            StartCoroutine(MakeDamage());
        }
    }
    private IEnumerator LifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private IEnumerator MakeDamage()
    {
        _isWorking = true;

        for (int i = 0; i < _objectsToDamage.Count; i++)
        {
            if (_objectsToDamage[i] == null)
            {
                _objectsToDamage.Remove(_objectsToDamage[i]);
                continue;
            }

            var healthComponent = _objectsToDamage[i].GetComponent<HealthComponent>();
            healthComponent.ApplyDamage(_damage, gameObject);
        }

        yield return new WaitForSeconds(_damageFrequency);
        _isWorking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isEnable && other.GetComponent<HealthComponent>() != null && _tagsToDamage.Contains(other.gameObject.tag))
            _objectsToDamage.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_objectsToDamage.Contains(other))
            _objectsToDamage.Remove(other);
    }
}