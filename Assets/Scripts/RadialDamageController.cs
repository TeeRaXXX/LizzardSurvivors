using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialDamageController : MonoBehaviour
{
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _damageFrequency = 1f;
    [SerializeField] private SOEnemy _enemy;
    [SerializeField] private SOCharacter _character;
    [SerializeField] private bool _isEnemy;
    [SerializeField] private bool _damageEnemies;
    [SerializeField] private bool _damagePlayer;

    private List<Collider2D> _objectsToDamage;
    private bool _isDamageInProcess = false;

    private void Awake()
    {
        _objectsToDamage = new List<Collider2D>();

        if (_isEnemy && _enemy != null)
        {
            _damage = _enemy.EnemyBaseStats.GetDamage();
            _damageFrequency = _enemy.EnemyBaseStats.GetAttackSpeed();
        }
    }

    private void FixedUpdate()
    {
        if (_objectsToDamage.Count > 0 && !_isDamageInProcess)
        {
            StartCoroutine(MakeDamage());
        }
    }

    private IEnumerator MakeDamage()
    {
        _isDamageInProcess = true;

        for (int i = 0; i < _objectsToDamage.Count; i++)
        {
            if (_objectsToDamage[i] == null)
            {
                _objectsToDamage.Remove(_objectsToDamage[i]);
                continue;
            }

            var healthComponent = _objectsToDamage[i].GetComponent<HealthComponent>();
            var isDead = healthComponent.ApplyDamage(_damage, this.gameObject);
        }

        yield return new WaitForSeconds(_damageFrequency);
        _isDamageInProcess = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null)
        {
            if (other.gameObject.tag == "Player" && _damagePlayer)
                _objectsToDamage.Add(other);
            if (other.gameObject.tag == "Enemy" && _damageEnemies)
                _objectsToDamage.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null)
        {
            _objectsToDamage.Remove(other);
        }
    }
}