using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SkillMusic : MonoBehaviour, IUpgradable
{
    [SerializeField] private float _coolDown = 1f;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private float _spawnAngle = 10;
    [SerializeField] private int _projectileCount = 3;
    [SerializeField] private float _projectileFrequency = 0.35f;

    private bool isAtacking = false;
    private PlayerMovement _playerMovement;
    private int _maxLevel;
    private int _currentLevel;

    private void Awake()
    {
        _maxLevel = 8;
        _currentLevel = 1;
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private IEnumerator AttackWithDelay()
    {
        isAtacking = true;
        float angle = -_spawnAngle;
        Vector3 playerLook = _playerMovement.GetLookDirection();

        for (int i = 0; i < _projectileCount; i++)
        {
            float spawnVectorX = playerLook.x * Mathf.Cos(angle / 360f) - playerLook.y * Mathf.Sin(angle / 360f);
            float spawnVectorY = playerLook.x * Mathf.Sin(angle / 360f) + playerLook.y * Mathf.Cos(angle / 360f);
            angle += _spawnAngle;
            var projectile = Instantiate(_projectile, _launchPoint.position, _launchPoint.rotation);
            projectile.GetComponent<ProjectileMusic>().Lounch(new Vector3(spawnVectorX, spawnVectorY, 0).normalized);
            yield return new WaitForSeconds(_projectileFrequency);
        }
        
        yield return new WaitForSeconds(_coolDown);
        isAtacking = false;
    }

    private void FixedUpdate()
    {
        if (!isAtacking)
        {
            StartCoroutine(AttackWithDelay());
        }
    }

    public void Upgrade()
    {
        if (_currentLevel < _maxLevel)
        {
            _currentLevel++;
            switch (_currentLevel)
            {
                case 2:
                    _projectileCount = 4;
                    break;

                case 3:
                    _coolDown = 0.35f;
                    break;

                case 4:
                    _projectileCount = 6;
                    break;

                case 5:
                    _coolDown = 0.25f;
                    break;

                case 6:
                    _projectileCount = 8;
                    break;

                case 7:
                    _coolDown = 0.15f;
                    break;

                case 8:
                    _coolDown = 0.0f;
                    break;
            }
        }
    }

    public int GetMaxLevel() => _maxLevel;
    public int GetCurrentLevel() => _currentLevel;
}