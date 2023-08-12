using NastyDoll.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMusicEvolved : MonoBehaviour, IUpgradable, IEvolvedSkill
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _launchPoint;

    private float _coolDown = 1f;
    private int _projectileCount = 3;
    private float _positionOffset = 2f;
    private float _positionOffset2 = 0f;
    private float _rotationSpeed = 4f;
    private bool isAtacking = false;
    private PlayerMovement _playerMovement;
    private float _projectileFrequency;
    private float _damage;
    private int _maxLevel;
    private int _currentLevel;
    private List<GameObject> _projectiles;
    private List<string> _tagsToDamage;


    public void Initialize(int playerIndex) 
    {
        isAtacking = true;
        _projectiles = new List<GameObject>();
        _maxLevel = 8;
        _damage = 10f;
        _tagsToDamage = new List<string>(UtilsClass.GetPlayerCharacter(playerIndex).TagsToDamage);
        _projectileCount = 3 + GlobalBonuses.Instance.GetAdditionalProjectilesCount();
        _playerMovement = UtilsClass.GetPlayerCharacter(playerIndex).PlayerMovement;
        _projectileFrequency = 360f / _projectileCount / _rotationSpeed;
        EventManager.OnProjectilesUpdate.AddListener(Upgrade);
    }

    public void Initialize(int level, int playerIndex)
    {
        _currentLevel = level;
        Upgrade(false);
        isAtacking = false;
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

            projectile.GetComponent<ProjectileMusicEvolved>().Lounch(_rotationSpeed, _launchPoint.gameObject, _projectileCount, _tagsToDamage, _damage);
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
                case 1:
                    _rotationSpeed = 100f;
                    _projectileCount = 3;
                    _damage = 10f;
                    break;
                    
                case 2:
                    _rotationSpeed = 100f;
                    _projectileCount = 4;
                    _damage = 10f;
                    break;

                case 3:
                    _rotationSpeed = 150f;
                    _projectileCount = 4;
                    _damage = 15f;
                    break;

                case 4:
                    _rotationSpeed = 150f;
                    _projectileCount = 5;
                    _damage = 15f;
                    break;

                case 5:
                    _rotationSpeed = 200f;
                    _projectileCount = 5;
                    _damage = 20f;
                    break;

                case 6:
                    _rotationSpeed = 200f;
                    _projectileCount = 6;
                    _damage = 20f;
                    break;

                case 7:
                    _rotationSpeed = 300f;
                    _projectileCount = 6;
                    _damage = 25f;
                    break;

                case 8:
                    _rotationSpeed = 300f;
                    _projectileCount = 7;
                    _damage = 25f;
                    break;

                default:
                    _rotationSpeed = 300f;
                    _projectileCount = 7;
                    _damage = 25f;
                    break;
            }

            _projectileCount += GlobalBonuses.Instance.GetAdditionalProjectilesCount();
            ResetProjectiles();
        }
    }
}