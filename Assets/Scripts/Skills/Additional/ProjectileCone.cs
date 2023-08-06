using NastyDoll.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileCone : MonoBehaviour
{
    private bool _isInited;
    private float _damage;
    private float _speed;
    private float _bounceCount;
    private float _damageRange;
    private List<string> _tagsToDamage;
    private GameObject _currentTarget;

    private void Awake() => _isInited = false;

    public void Initialize(float damage, float bounceCount, float speed, List<string> tagsToDamage)
    {
        Debug.Log("Spawn Cone");
        _damageRange = 1f;
        _damage = damage;
        _speed = speed;
        _bounceCount = bounceCount;
        _tagsToDamage = new List<string>(tagsToDamage);
        _tagsToDamage.Shuffle();
        List<GameObject> enemies = null;

        foreach (var enemieTag in tagsToDamage)
        {
            enemies = GameObject.FindGameObjectsWithTag(enemieTag).ToList();
            if (enemies != null) break;
        }

        if (enemies != null && enemies.Count > 0)
        {
            enemies.Shuffle();
            _currentTarget = enemies[0];
        }

        _isInited = true;
    }

    private void Update()
    {
        if (_isInited)
        {
            if (_currentTarget != null)
            {
                if (Vector3.Distance(_currentTarget.transform.position, transform.position) <= _damageRange)
                {
                    ApplyDamage();
                    UpdateTarget();
                }
                Vector3 moveVector = (_currentTarget.transform.position - transform.position).normalized;
                transform.position = transform.transform.position + moveVector * _speed * Time.deltaTime;
            }
            else Destroy(gameObject);
        }
    }

    private void ApplyDamage()
    {
        HealthComponent healthComponent = null;
        _currentTarget.TryGetComponent<HealthComponent>(out healthComponent);
        if (healthComponent != null)
            healthComponent.ApplyDamage(_damage, gameObject);
    }

    private void UpdateTarget()
    {
        if (_bounceCount <= 0 )
            Destroy(gameObject);

        _bounceCount--;

        List<GameObject> enemies = new List<GameObject>();

        foreach (var enemieTag in _tagsToDamage)
        {
            enemies.AddRange(GameObject.FindGameObjectsWithTag(enemieTag).ToList());
        }

        if (enemies.Count == 0)
        {
            Destroy(gameObject);
        }

        _currentTarget = UtilsClass.GetNearestObjectExcept(transform.position, enemies, _currentTarget);
        if (_currentTarget == null)
        {
            Destroy(gameObject);
        }
    }
}