using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    private float _health;
    private float _maxHealth;
    private float _armor;
    private UnityEvent<float, GameObject> _onHealthChangedEvent;

    public void InitHealth(float maxHealth, float armor, UnityEvent<float, GameObject> onHealthChangedEvent)
    {
        _health = maxHealth;
        _maxHealth = maxHealth;
        _armor = armor;
        _onHealthChangedEvent = onHealthChangedEvent;
    }

    public bool ApplyDamage(float damageValue, GameObject damageSource)
    {
        if (damageValue > 0f)
        {
            float totalDamage = _armor > 0 ? damageValue * _armor / 100f : damageValue;
            float oldHealth = _health;

            if (_health - totalDamage <= 0)
            {
                _health = 0f;
                if (damageSource != null)
                    _onHealthChangedEvent.Invoke(_health, damageSource);
                else _onHealthChangedEvent.Invoke(_health, null);
                return true;
            }
            else
            {
                _health -= totalDamage;
                _onHealthChangedEvent.Invoke(_health, damageSource);
                return false;
            }
        }
        else throw new Exception($"Exception: {damageSource.name} try to hit with {damageValue} damage!");
    }

    public void ApplyHeal(float healValue, GameObject healSource)
    {
        if (_health > 0f && _health < _maxHealth)
        {
            float oldHealth = _health;
            _health = Mathf.Clamp(_health + healValue, _health, _maxHealth);
            _onHealthChangedEvent.Invoke(_health, healSource);
        }
    }

    public void UpdateStats(float maxHealth, float armor)
    {
        _maxHealth = maxHealth;
        _armor = armor;
    }

    public float GetHealth()
    {
        return _health;
    }
}