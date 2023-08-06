using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTotemProjectiles : MonoBehaviour, IUpgradable
{
    [SerializeField] private GameObject _totemPrefab;

    private int _maxLevel;
    private int _currentLevel;

    private List<string> _tagsToDamage;
    private float _spawnFrequency;
    private float _shootingFrequency;
    private float _totemLifeTime;
    private float _damage;
    private float _projectileSpeed;
    private int _projectilesCount;

    private bool _isActive;

    public void Initialize(int playerIndex)
    {
        _isActive = true;

        EventManager.OnProjectilesUpdate.AddListener(Upgrade);
        _tagsToDamage = new List<string>(TagsHandler.GetEnemyTags());
        _spawnFrequency = 3f;
        _totemLifeTime = 3f;
        _shootingFrequency = 1f;
        _damage = 25f;
        _projectileSpeed = 150f;
        _projectilesCount = 1 + GlobalBonuses.Instance.GetAdditionalProjectilesCount();

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

        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-5f, 5f),
                                            transform.position.y + Random.Range(-5f, 5f),
                                            0f);

        if (spawnPosition != Vector3.zero)
        {
            var totem = Instantiate(_totemPrefab, spawnPosition, Quaternion.identity);
            totem.GetComponent<TotemProjectiles>().Initialize(_tagsToDamage,
                                                              _damage,
                                                              _shootingFrequency,
                                                              _totemLifeTime,
                                                              _projectileSpeed,
                                                              _projectilesCount);
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
                    _damage = 35f;
                    _projectilesCount = 1;
                    break;

                case 3:
                    _damage = 35f;
                    _projectilesCount = 2;
                    break;

                case 4:
                    _damage = 45f;
                    _projectilesCount = 2;
                    break;

                case 5:
                    _damage = 45f;
                    _projectilesCount = 3;
                    break;

                case 6:
                    _damage = 60f;
                    _projectilesCount = 3;
                    break;

                case 7:
                    _damage = 60f;
                    _projectilesCount = 4;
                    break;

                case 8:
                    _damage = 80f;
                    _projectilesCount = 4;
                    break;
            }

            _projectilesCount += GlobalBonuses.Instance.GetAdditionalProjectilesCount();
        }
    }
}