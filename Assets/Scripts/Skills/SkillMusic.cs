using NastyDoll.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<string> _tagsToDamage;
    private float _damage;
    private float _projectileSpeed;

    public void Initialize(int playerIndex)
    {
        _maxLevel = 8;
        _currentLevel = 1;
        _projectileFrequency = 0.25f;
        _damage = 25f;
        _projectileSpeed = 200f;
        _tagsToDamage = new List<string>(UtilsClass.GetPlayerCharacter(playerIndex).TagsToDamage);
        _projectileCount = 3 + GlobalBonuses.Instance.GetAdditionalProjectilesCount();
        _playerMovement = UtilsClass.FindObjectsWithTagsList(TagsHandler.GetPlayerTags()).
            FirstOrDefault(p => p.GetComponent<PlayerCharacter>().PlayerIndex == playerIndex).GetComponent<PlayerMovement>();
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
            projectile.GetComponent<ProjectileMusic>().Lounch(new Vector3(spawnVectorX, spawnVectorY, 0).normalized, _damage, _projectileSpeed, _tagsToDamage);
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
                case 1:
                    _projectileCount = 3;
                    _coolDown = 0.35f;
                    _projectileFrequency = 0.25f;
                    break;
                    
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
                    _spawnAngle = 360f / _projectileFrequency * (_projectileCount + GlobalBonuses.Instance.GetAdditionalProjectilesCount());
                    break;

                default:
                    _projectileCount = 8;
                    _coolDown = 0.0f;
                    _projectileFrequency = 0.05f;
                    _spawnAngle = 360f / _projectileFrequency * (_projectileCount + GlobalBonuses.Instance.GetAdditionalProjectilesCount());
                    break;
            }

            _projectileCount += GlobalBonuses.Instance.GetAdditionalProjectilesCount();
        }
    }

    public int GetMaxLevel() => _maxLevel;
    public int GetCurrentLevel() => _currentLevel;
}