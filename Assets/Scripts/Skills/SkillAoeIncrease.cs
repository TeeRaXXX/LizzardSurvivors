using UnityEngine;

public class SkillAoeIncrease : MonoBehaviour, IUpgradable
{
    private int _maxLevel;
    private int _currentLevel;

    private float _aoeMultiplier;

    private void Awake()
    {
        _currentLevel = 1;
        _maxLevel = 8;

        _aoeMultiplier = 1.1f;
        GlobalBonuses.Instance.UpdateAdditionalAoeRadius(_aoeMultiplier);
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
                    _aoeMultiplier = 1.2f;
                    break;

                case 3:
                    _aoeMultiplier = 1.3f;
                    break;

                case 4:
                    _aoeMultiplier = 1.4f;
                    break;

                case 5:
                    _aoeMultiplier = 1.5f;
                    break;

                case 6:
                    _aoeMultiplier = 1.6f;
                    break;

                case 7:
                    _aoeMultiplier = 1.7f;
                    break;

                case 8:
                    _aoeMultiplier = 1.8f;
                    break;
            }

            GlobalBonuses.Instance.UpdateAdditionalAoeRadius(_aoeMultiplier);
        }
    }
}