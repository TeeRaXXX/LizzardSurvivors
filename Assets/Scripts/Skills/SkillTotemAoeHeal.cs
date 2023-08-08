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

    public void Initialize(int playerIndex)
    {
        _isActive = true;

        _tagsToHeal = new List<string>()
        {
            TagsHandler.GetPlayerTag()
        };

        _spawnFrequency = 3f;
        _totemLifeTime = 3f;
        _healFrequency = 0.5f;
        _heal = 1f;
        _healRadius = 1f * GlobalBonuses.Instance.GetAdditionalAoeRadius();

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

        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f),
                                            transform.position.y + Random.Range(-1f, 1f),
                                            0);

        var totem = Instantiate(_totemPrefab, spawnPosition, Quaternion.identity);
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
                case 1:
                    _heal = 1f;
                    _healFrequency = 0.5f;
                    _healRadius = 1f;
                    break;
                    
                case 2:
                    _heal = 1f;
                    _healFrequency = 0.45f;
                    _healRadius = 1f;
                    break;

                case 3:
                    _heal = 2f;
                    _healFrequency = 0.45f;
                    _healRadius = 1f;
                    break;

                case 4:
                    _heal = 2f;
                    _healFrequency = 0.4f;
                    _healRadius = 1f;
                    break;

                case 5:
                    _heal = 3f;
                    _healFrequency = 0.4f;
                    _healRadius = 1f;
                    break;

                case 6:
                    _heal = 3f;
                    _healFrequency = 0.35f;
                    _healRadius = 1f;
                    break;

                case 7:
                    _heal = 4f;
                    _healFrequency = 0.35f;
                    _healRadius = 1f;
                    break;

                case 8:
                    _heal = 4f;
                    _healFrequency = 0.3f;
                    _healRadius = 1f;
                    break;

                default:
                    _heal = 4f;
                    _healFrequency = 0.3f;
                    _healRadius = 1f;
                    break;
            }

            _healRadius *= GlobalBonuses.Instance.GetAdditionalAoeRadius();
        }
    }
}