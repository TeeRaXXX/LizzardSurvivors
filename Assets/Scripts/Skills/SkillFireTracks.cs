using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFireTracks : MonoBehaviour, IUpgradable
{
    [SerializeField] private GameObject _fireTrackPrefab;

    private List<string> _tagsToDamage;
    private float _spawnFrequency;
    private float _damageFrequency;
    private float _fireTrackLifeTime;
    private float _damageRadius;
    private float _damage;
    private int _maxLevel;
    private int _currentLevel;
    private bool _isActive;

    public void Initialize(int playerIdnex)
    {
        _isActive = true;

        _tagsToDamage = new List<string>(TagsHandler.GetEnemyTags());
        _spawnFrequency = 0.4f;
        _fireTrackLifeTime = 3f;
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
            StartCoroutine(SpawnFireTrack());
        }
    }

    private IEnumerator SpawnFireTrack()
    {
        _isActive = true;

        var fireTrack = Instantiate(_fireTrackPrefab, transform.position, Quaternion.identity);
        fireTrack.GetComponent<FireTrack>().Initialize(_tagsToDamage, _damageRadius, _damage, _damageFrequency, _fireTrackLifeTime);
 
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
                case 1:
                    _damage = 10f;
                    _damageRadius = 1f;
                    _spawnFrequency = 0.4f;
                    break;

                case 2:
                    _damage = 15f;
                    _damageRadius = 1f;
                    _spawnFrequency = 0.4f;
                    break;

                case 3:
                    _damage = 15f;
                    _damageRadius = 1.1f;
                    _spawnFrequency = 0.3f;
                    break;

                case 4:
                    _damage = 20f;
                    _damageRadius = 1.1f;
                    _spawnFrequency = 0.3f;
                    break;

                case 5:
                    _damage = 20f;
                    _damageRadius = 1.2f;
                    _spawnFrequency = 0.2f;
                    break;

                case 6:
                    _damage = 25f;
                    _damageRadius = 1.2f;
                    _spawnFrequency = 0.2f;
                    break;

                case 7:
                    _damage = 25f;
                    _damageRadius = 1.3f;
                    _spawnFrequency = 0.1f;
                    break;

                case 8:
                    _damage = 30f;
                    _damageRadius = 1.3f;
                    _spawnFrequency = 0.1f;
                    break;

                default:
                    _damage = 30f;
                    _damageRadius = 1.4f;
                    _spawnFrequency = 0.1f;
                    break;
            }

            _damageRadius *= GlobalBonuses.Instance.GetAdditionalAoeRadius();
        }
    }
}