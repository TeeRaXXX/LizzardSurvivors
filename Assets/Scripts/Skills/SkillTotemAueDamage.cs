using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTotemAoeDamage : MonoBehaviour, IUpgradable
{
    [SerializeField] private GameObject _totemPrefab;

    private List<string> _tagsToDamage;
    private float _spawnFrequency;
    private float _damageFrequency;
    private float _totemLifeTime;
    private float _damageRadius;
    private float _damage;
    private int _maxLevel;
    private int _currentLevel;
    private bool _isActive;

    private void Awake()
    {
        _isActive = true;

        _tagsToDamage = new List<string>(TagsHandler.GetEnemyTags());
        _spawnFrequency = 3f;
        _totemLifeTime = 3f;
        _damageFrequency = .5f;
        _damageRadius = 2f;
        _damage = 10f;

        _maxLevel = 8;
        _currentLevel = 1;

        _isActive = false;
    }

    private void Update()
    {
        if (!_isActive)
        {
            StartCoroutine(SpawnTotem());
        }
    }

    private IEnumerator SpawnTotem()
    {
        _isActive = true;

        Vector3 spawnPosition = Vector3.zero;

        foreach (var enemyTag in TagsHandler.GetEnemyTags())
        {
            GameObject enemy = GameObject.FindGameObjectWithTag(enemyTag);

            if (enemy != null) 
            {
                spawnPosition = enemy.transform.position;
                break;
            }
        }

        if (spawnPosition != Vector3.zero)
        {
            var totem = Instantiate(_totemPrefab, spawnPosition, Quaternion.identity);
            totem.GetComponent<TotemAoeDamage>().Initialize(_tagsToDamage, _damageRadius, _damage, _damageFrequency, _totemLifeTime);
        }
        
        yield return new WaitForSeconds(_spawnFrequency);

        _isActive = false;
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
                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;

                case 5:
                    break;

                case 6:
                    break;

                case 7:
                    break;

                case 8:
                    break;
            }
        }
    }
}