using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TotemType
{
    TotemAoeDamage,
    TotemProjectiles,
    TotemAoeHeal
}

public class SkillTotem : MonoBehaviour, IUpgradable
{
    [SerializeField] private float _spawnCoolDown;
    [SerializeField] private List<GameObject> _totems;

    private int _maxLevel;
    private int _currentLevel;
    private bool _isActive;

    private int _currentTotemIndex;

    private void Awake()
    {
        _isActive = true;
        _spawnCoolDown = 3f;
        _maxLevel = 8;
        _currentLevel = 1;
        _currentTotemIndex = 0;
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

        Instantiate(_totems[_currentTotemIndex], transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(_spawnCoolDown);

        if (_currentTotemIndex == _totems.Count - 1)
            _currentTotemIndex = 0;
        else _currentTotemIndex++;

        _isActive = false;
    }

    private TotemType GetRandomTotem()
    {
        var values = System.Enum.GetValues(typeof(TotemType)); 
        return (TotemType)values.GetValue(Random.Range(0, values.Length));
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