using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemProjectiles : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _spawnPosition;

    private List<string> _tagsToDamage;
    private float _damage = 5f;
    private float _lifeTime;
    private float _spawnFrequency;
    private float _projectileSpeed;
    private int _projectilesCount;

    private bool _isReadyToWork;

    private GameObject _radialDamageInstance;

    private void Awake() => _isReadyToWork = false;

    public void Initialize(List<string> tagsToDamage, float damage, float shootingFrequency, float lifeTime, float projectileSpeed, int projectilesCount)
    {
        _tagsToDamage = new List<string>(tagsToDamage);
        _lifeTime = lifeTime;
        _damage = damage;
        _spawnFrequency = shootingFrequency;
        _projectileSpeed = projectileSpeed;
        _projectilesCount = projectilesCount;

        _isReadyToWork = true;

        StartCoroutine(OnLifeTime());
    }

    private void Update()
    {
        if (_isReadyToWork)
        {
            StartCoroutine(SpawnProjectile());
        }
    }

    private IEnumerator SpawnProjectile()
    {
        _isReadyToWork = false;

        for (int i = 0; i < _projectilesCount; i++)
        {
            GameObject projectile = Instantiate(_projectilePrefab, _spawnPosition.position, Quaternion.identity, transform);
            projectile.GetComponent<ProjectileTotem>().Initialize(_tagsToDamage, _damage, _projectileSpeed);
        }

        yield return new WaitForSeconds(_spawnFrequency);

        _isReadyToWork = true;
    }

    private IEnumerator OnLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        _animator.SetBool("IsDeath", true);
        Destroy(_radialDamageInstance);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}