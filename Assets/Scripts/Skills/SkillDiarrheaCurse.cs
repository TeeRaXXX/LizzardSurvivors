using NastyDoll.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDiarrheaCurse : MonoBehaviour, IUpgradable
{
    [SerializeField] private GameObject _cursePrefab;

    private List<string> _tagsToDamage;
    private int _maxLevel;
    private int _currentLevel;
    private float _spawnFrequency;
    private float _curseLifeTime;
    private float _damageFrequency;
    private float _damageRadius;
    private float _damage;
    private float _moveSpeedReducePercent;
    private bool _isActive;

    public void Initialize(int playerIndex)
    {
        _isActive = false;

        _tagsToDamage = new List<string>(UtilsClass.GetPlayerCharacter(playerIndex).TagsToDamage);
        _maxLevel = 8;
        _currentLevel = 1;
        _spawnFrequency = 1.5f;
        _curseLifeTime = 2f;
        _damageFrequency = 0.3f;
        _damage = 5f;
        _moveSpeedReducePercent = 0.4f;
        _damageRadius = 1f * GlobalBonuses.Instance.GetAdditionalAoeRadius();
        EventManager.OnAoeUpdate.AddListener(Upgrade);

        _isActive = true;
    }

    private void Update()
    {
        if (_isActive)
        {
            StartCoroutine(SpawnCurse());
        }
    }

    private IEnumerator SpawnCurse()
    {
        _isActive = false;
        Vector3 spawnPosition = Vector3.zero;
        _tagsToDamage.Shuffle();

        foreach (var enemyTag in _tagsToDamage)
        {
            var enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag(enemyTag));
            enemies.Shuffle();

            if (enemies.Count > 0)
            {
                spawnPosition = enemies[Random.Range(0, enemies.Count - 1)].transform.position;
                break;
            }
        }

        if (spawnPosition != Vector3.zero)
        {
            var curse = Instantiate(_cursePrefab, spawnPosition, Quaternion.identity);
            curse.GetComponent<RadialDamage>().Initialize(_tagsToDamage, _damageRadius, _damage, _damageFrequency, _curseLifeTime);
            curse.GetComponent<RadialMoveSpeedDecrease>().Initialize(_moveSpeedReducePercent, _tagsToDamage, _damageRadius, _curseLifeTime);
        }

        yield return new WaitForSeconds(_spawnFrequency);

        _isActive = true;
    }

    public int GetCurrentLevel() => _currentLevel;

    public int GetMaxLevel() => _maxLevel;

    public void Upgrade(bool isNewLevel)
    {
        if (isNewLevel)
            _currentLevel++;

        if (_currentLevel <= _maxLevel)
        {
            switch (_currentLevel)
            {
                case 1:
                    _spawnFrequency = 1.5f;
                    _moveSpeedReducePercent = 0.4f;
                    _damageRadius = 1f;
                    _damage = 5f;
                    break;
                    
                case 2:
                    _spawnFrequency = 1.5f;
                    _moveSpeedReducePercent = 0.45f;
                    _damageRadius = 1.2f;
                    _damage = 5f;
                    break;

                case 3:
                    _spawnFrequency = 1.5f;
                    _moveSpeedReducePercent = 0.5f;
                    _damageRadius = 1.2f;
                    _damage = 7f;
                    break;

                case 4:
                    _spawnFrequency = 1.5f;
                    _moveSpeedReducePercent = 0.5f;
                    _damageRadius = 1.3f;
                    _damage = 7f;
                    break;

                case 5:
                    _spawnFrequency = 1f;
                    _moveSpeedReducePercent = 0.55f;
                    _damageRadius = 1.3f;
                    _damage = 9f;
                    break;

                case 6:
                    _spawnFrequency = 1f;
                    _moveSpeedReducePercent = 0.6f;
                    _damageRadius = 1.4f;
                    _damage = 9f;
                    break;

                case 7:
                    _spawnFrequency = 1f;
                    _moveSpeedReducePercent = 0.6f;
                    _damageRadius = 1.4f;
                    _damage = 11f;
                    break;

                case 8:
                    _spawnFrequency = 0.5f;
                    _moveSpeedReducePercent = 0.7f;
                    _damageRadius = 1.5f;
                    _damage = 11f;
                    break;

                default:
                    _spawnFrequency = 0.5f;
                    _moveSpeedReducePercent = 0.7f;
                    _damageRadius = 1.5f;
                    _damage = 11f;
                    break;
            }

            _damageRadius *= GlobalBonuses.Instance.GetAdditionalAoeRadius();
        }
    }
}