using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRoots : MonoBehaviour, IUpgradable
{
    [SerializeField] private GameObject _effectsPrefab;

    private RadialMoveSpeedDecrease _radialMoveSpeedDecrease;
    private RadialDamage _radialDamage;
    private List<string> _tagsToDamage;
    private int _maxLevel;
    private int _currentLevel;
    private float _damageFrequency;
    private float _damageRadius;
    private float _damage;
    private float _moveSpeedReducePercent;

    private void Awake()
    {
        _currentLevel = 1;
        _maxLevel = 8;

        _moveSpeedReducePercent = 0.45f;
        _damageRadius = 1f;
        _damage = 10f;
        _damageFrequency = 0.3f;
        _tagsToDamage = new List<string>(TagsHandler.GetEnemyTags());

        var _prefab = Instantiate(_effectsPrefab, transform);

        _radialDamage = _prefab.GetComponent<RadialDamage>();
        _radialDamage.Initialize(_tagsToDamage, _damageRadius, _damage, _damageFrequency);

        _radialMoveSpeedDecrease = _radialDamage.GetComponent<RadialMoveSpeedDecrease>();
        _radialMoveSpeedDecrease.Initialize(_moveSpeedReducePercent, _tagsToDamage, _damageRadius);

        EventManager.OnAoeUpdate.AddListener(Upgrade);
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
                    _moveSpeedReducePercent = 0.45f;
                    _damageRadius = 1.25f;
                    _damage = 10f;
                    _damageFrequency = 0.3f;
                    break;

                case 3:
                    _moveSpeedReducePercent = 0.5f;
                    _damageRadius = 1.25f;
                    _damage = 15f;
                    _damageFrequency = 0.25f;
                    break;

                case 4:
                    _moveSpeedReducePercent = 0.5f;
                    _damageRadius = 1.35f;
                    _damage = 15f;
                    _damageFrequency = 0.25f;
                    break;

                case 5:
                    _moveSpeedReducePercent = 0.55f;
                    _damageRadius = 1.35f;
                    _damage = 20f;
                    _damageFrequency = 0.2f;
                    break;

                case 6:
                    _moveSpeedReducePercent = 0.6f;
                    _damageRadius = 1.45f;
                    _damage = 20f;
                    _damageFrequency = 0.2f;
                    break;

                case 7:
                    _moveSpeedReducePercent = 0.6f;
                    _damageRadius = 1.45f;
                    _damage = 25f;
                    _damageFrequency = 0.15f;
                    break;

                case 8:
                    _moveSpeedReducePercent = 0.7f;
                    _damageRadius = 1.6f;
                    _damage = 25f;
                    _damageFrequency = 0.15f;
                    break;
            }

            _damageRadius *= GlobalBonuses.Instance.GetAdditionalAoeRadius();
            _radialDamage.UpdateStats(_tagsToDamage, _damageRadius, _damage, _damageFrequency);
            _radialMoveSpeedDecrease.UpdateStats(_moveSpeedReducePercent, _tagsToDamage, _damageRadius);
        }
    }
}