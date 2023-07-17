using System.Collections;
using UnityEngine;

public sealed class SkillMusic : MonoBehaviour, IUpgradable
{
    [SerializeField] private float _coolDown = 1f;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private float _spawnAngle = 10;
    [SerializeField] private int _projectileCount = 3;
    [SerializeField] private float _projectileFrequency = 0.25f;

    private bool isAtacking = false;
    private PlayerMovement _playerMovement;
    private int _maxLevel;
    private int _currentLevel;

    private void Awake()
    {
        _maxLevel = 8;
        _currentLevel = 1;
        _projectileFrequency = 0.25f;
        _projectileCount = 3 + GlobalBonuses.Instance.GetAdditionalProjectilesCount();
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        EventManager.OnProjectilesUpdate.AddListener(Upgrade);
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
                    _coolDown = 0.35f;
                    _projectileFrequency = 0.25f;
                    break;

                case 3:
                    _projectileCount = 4;
                    _coolDown = 0.35f;
                    _projectileFrequency = 0.2f;
                    break;

                case 4:
                    _projectileCount = 6;
                    _coolDown = 0.35f;
                    _projectileFrequency = 0.2f;
                    break;

                case 5:
                    _projectileCount = 6;
                    _coolDown = 0.25f;
                    _projectileFrequency = 0.15f;
                    break;

                case 6:
                    _projectileCount = 8;
                    _coolDown = 0.25f;
                    _projectileFrequency = 0.15f;
                    break;

                case 7:
                    _projectileCount = 8;
                    _coolDown = 0.15f;
                    _projectileFrequency = 0.1f;
                    break;

                case 8:
                    _projectileCount = 8;
                    _coolDown = 0.0f;
                    _projectileFrequency = 0.05f;
                    break;
            }

            _projectileCount += GlobalBonuses.Instance.GetAdditionalProjectilesCount();
        }
    }

    public int GetMaxLevel() => _maxLevel;
    public int GetCurrentLevel() => _currentLevel;
}