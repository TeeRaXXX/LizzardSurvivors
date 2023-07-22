using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialDamage : MonoBehaviour
{
    private List<string> _tagsToDamage;
    private float _damage = 5f;
    private float _damageFrequency = 1f;
    private float _damageRadius;

    private List<Collider2D> _objectsToDamage;
    private bool _isReadyToWork = false;

    private void Awake()
    {
        _isReadyToWork = false;
        _objectsToDamage = new List<Collider2D>();
    }

    public void Initialize(List<string> tagsToDamage, float damageRadius, float damage, float damageFrequency)
    {
        UpdateStats(tagsToDamage, damageRadius, damage, damageFrequency);
        _isReadyToWork = true;
    }

    public void UpdateStats(List<string> tagsToDamage, float damageRadius, float damage, float damageFrequency)
    {
        _tagsToDamage = new List<string>(tagsToDamage);
        _damage = damage;
        _damageFrequency = damageFrequency;
        _damageRadius = damageRadius;
    }

    private void FixedUpdate()
    {
        if (_objectsToDamage.Count > 0 && _isReadyToWork)
        {
            StartCoroutine(MakeDamage());
        }
    }

    private IEnumerator MakeDamage()
    {
        _isReadyToWork = false;

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
        _isReadyToWork = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isReadyToWork && other.GetComponent<HealthComponent>() != null && _tagsToDamage.Contains(other.gameObject.tag))
            _objectsToDamage.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_objectsToDamage.Contains(other))
            _objectsToDamage.Remove(other);
    }
}