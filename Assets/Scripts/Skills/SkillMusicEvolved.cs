using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMusicEvolved : MonoBehaviour, IUpgradable
{
    [SerializeField] private float _coolDown = 1f;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private int _projectileCount = 3;
    [SerializeField] private float _positionOffset = 1.5f;
    [SerializeField] private float _positionOffset2 = 0.1f;
    [SerializeField] private float _rotationSpeed = 4f;

    private bool isAtacking = false;
    private PlayerMovement _playerMovement;
    private float _projectileFrequency;
    private int _maxLevel;
    private int _currentLevel;
    private List<GameObject> _projectiles;

    private void Awake()
    {
        _projectiles = new List<GameObject>();
        _currentLevel = 1;
        _maxLevel = 8;
        _projectileCount = 3 + GlobalBonuses.Instance.GetAdditionalProjectilesCount();
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _projectileFrequency = 360f / _projectileCount / _rotationSpeed;
        EventManager.OnProjectilesUpdate.AddListener(Upgrade);
    }

    private IEnumerator AttackWithDelay()
    {
        isAtacking = true;
        Vector3 playerLook = _playerMovement.GetLookDirection();
        bool addOffset = false;

        for (int i = 0; i < _projectileCount; i++)
        {
            float x = _launchPoint.position.x + _positionOffset;
            if (addOffset)
                x = _launchPoint.position.x + _positionOffset + _positionOffset2;

            var projectile = Instantiate(_projectile, new Vector3(
                x,
                _launchPoint.position.y,
                0f),
                new Quaternion(0f, 0f, 0f, 1),
                _launchPoint.transform);

            _projectiles.Add(projectile);

            projectile.GetComponent<ProjectileMusicEvolved>().Lounch(_rotationSpeed, _launchPoint.gameObject, _projectileCount);
            addOffset = !addOffset;
            yield return new WaitForSeconds(_projectileFrequency);
        }
        
        yield return new WaitForSeconds(_coolDown);
    }

    private void FixedUpdate()
    {
        if (!isAtacking)
        {
            StartCoroutine(AttackWithDelay());
        }
        _launchPoint.transform.Rotate(new Vector3(0f, 0f, _rotationSpeed) * Time.deltaTime);
    }

    private void ResetProjectiles()
    {
        foreach (var projectile in _projectiles)
        {
            Destroy(projectile);
        }

        _projectiles.Clear();
        _projectileFrequency = 360f / _projectileCount / _rotationSpeed;
        isAtacking = false;
    }

    public int GetMaxLevel() => _maxLevel;

    public int GetCurrentLevel() => _currentLevel;

    public void Upgrade(bool isNewLevel)
    {
        if (isNewLevel)
            _currentLevel++;

        if (_currentLevel <= _maxLevel)
        {
            switch (_currentLevel)
            {
                case 2:
                    _projectileCount = 4;
                    break;

                case 3:
                    _rotationSpeed = 150f;
                    break;

                case 4:
                    _projectileCount = 5;
                    break;

                case 5:
                    _rotationSpeed = 200f;
                    break;

                case 6:
                    _projectileCount = 6;
                    break;

                case 7:
                    _rotationSpeed = 300f;
                    break;

                case 8:
                    _projectileCount = 7;
                    break;
            }

            _projectileCount += GlobalBonuses.Instance.GetAdditionalProjectilesCount();
            ResetProjectiles();
        }
    }
}