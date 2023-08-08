using NastyDoll.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillCones : MonoBehaviour, IUpgradable
{
    [SerializeField] private GameObject _conePrefab;

    private int _maxLevel;
    private int _currentLevel;
    private int _bounceCount;
    private int _spawnCount;
    private float _coolDown;
    private float _damage;
    private float _speed;
    private bool _isReadyToWork;
    private List<string> _tagsToDamage;

    public void Initialize(int playerIndex)
    {
        _isReadyToWork = false;
        _currentLevel = 1;
        _maxLevel = 8;
        _bounceCount = 3;
        _damage = 15f;
        _speed = 5f;
        _coolDown = 1f;
        _spawnCount = 1 + GlobalBonuses.Instance.GetAdditionalProjectilesCount();
        _tagsToDamage = new List<string>(TagsHandler.GetEnemyTags());
        EventManager.OnProjectilesUpdate.AddListener(Upgrade);
        _isReadyToWork = true;
    }

    private IEnumerator SpawnCone(float cooldown)
    {
        _isReadyToWork = false;

        for (int i = 0; i < _spawnCount; i++)
        {
            ProjectileCone cone = Instantiate(_conePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileCone>();
            cone.Initialize(_damage, _bounceCount, _speed, _tagsToDamage);
        }

        yield return new WaitForSeconds(cooldown);
        _isReadyToWork = true;
    }

    private void Update()
    {
        if (_isReadyToWork)
        {
            StartCoroutine(SpawnCone(_coolDown));
        }
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
                    _damage = 15f;
                    _bounceCount = 3;
                    _spawnCount = 1;
                    break;
                    
                case 2:
                    _damage = 15f;
                    _bounceCount = 4;
                    _spawnCount = 1;
                    break;

                case 3:
                    _damage = 20f;
                    _bounceCount = 4;
                    _spawnCount = 1;
                    break;

                case 4:
                    _damage = 20f;
                    _bounceCount = 6;
                    _spawnCount = 1;
                    break;

                case 5:
                    _damage = 25f;
                    _bounceCount = 6;
                    _spawnCount = 1;
                    break;

                case 6:
                    _damage = 25f;
                    _bounceCount = 8;
                    _spawnCount = 1;
                    break;

                case 7:
                    _damage = 30f;
                    _bounceCount = 8;
                    _spawnCount = 1;
                    break;

                case 8:
                    _damage = 30f;
                    _bounceCount = 10;
                    _spawnCount = 1;
                    break;

                default:
                    _damage = 30f;
                    _bounceCount = 10;
                    _spawnCount = 1;
                    break;
            }

            _spawnCount += GlobalBonuses.Instance.GetAdditionalProjectilesCount();
        }
    }
}