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

    public void Initialize(int playerIndex)
    {
        _isActive = true;

        _tagsToDamage = new List<string>(TagsHandler.GetEnemyTags());
        _spawnFrequency = 3f;
        _totemLifeTime = 3f;
        _damageFrequency = .5f;
        _damage = 10f;
        _damageRadius = 1f * GlobalBonuses.Instance.GetAdditionalAoeRadius();

        _maxLevel = 8;
        _currentLevel = 1;

        EventManager.OnAoeUpdate.AddListener(Upgrade);

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
            spawnPosition = new Vector3(spawnPosition.x + Random.Range(-2f, 2f),
                                        spawnPosition.y + Random.Range(-2f, 2f),
                                        0);

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
                    _damage = 15f;
                    _damageRadius = 1f;
                    break;

                case 3:
                    _damage = 15f;
                    _damageRadius = 1.05f;
                    break;

                case 4:
                    _damage = 20f;
                    _damageRadius = 1.05f;
                    break;

                case 5:
                    _damage = 20f;
                    _damageRadius = 1.1f;
                    break;

                case 6:
                    _damage = 25f;
                    _damageRadius = 1.1f;
                    break;

                case 7:
                    _damage = 25f;
                    _damageRadius = 1.15f;
                    break;

                case 8:
                    _damage = 30f;
                    _damageRadius = 1.15f;
                    break;
            }

            _damageRadius *= GlobalBonuses.Instance.GetAdditionalAoeRadius();
        }
    }
}