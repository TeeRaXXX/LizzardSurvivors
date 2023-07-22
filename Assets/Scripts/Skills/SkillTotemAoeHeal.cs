using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTotemAoeHeal : MonoBehaviour, IUpgradable
{
    [SerializeField] private GameObject _totemPrefab;

    private List<string> _tagsToHeal;
    private float _spawnFrequency;
    private float _healFrequency;
    private float _totemLifeTime;
    private float _healRadius;
    private float _heal;
    private int _maxLevel;
    private int _currentLevel;
    private bool _isActive;

    private void Awake()
    {
        _isActive = true;

        _tagsToHeal = new List<string>()
        {
            TagsHandler.GetPlayerTag()
        };

        _spawnFrequency = 3f;
        _totemLifeTime = 3f;
        _healFrequency = .5f;
        _healRadius = 2f;
        _heal = 0.5f;

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

        var totem = Instantiate(_totemPrefab, transform.position, Quaternion.identity);
        totem.GetComponent<TotemAoeHeal>().Initialize(_tagsToHeal, _healRadius, _heal, _healFrequency, _totemLifeTime);

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
