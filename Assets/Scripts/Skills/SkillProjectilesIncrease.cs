using UnityEngine;

public class SkillProjectilesIncrease : MonoBehaviour, IUpgradable
{
    private int _additionalProjectilesCount;
    private int _maxLevel;
    private int _currentLevel;

    public void Initialize(int playerIndex)
    {
        _maxLevel = 8;
        _currentLevel = 1;
        _additionalProjectilesCount = 1;
        GlobalBonuses.Instance.UpdateAdditionalProjectilesCount(_additionalProjectilesCount);
    }

    public int GetMaxLevel() => _currentLevel;

    public int GetCurrentLevel() => _currentLevel;

    public void Upgrade(bool isNewLevel)
    {
        if (isNewLevel)
            _currentLevel++;

        if (_currentLevel <= _maxLevel)
        {
            switch (_currentLevel)
            {
                case 1:
                    _additionalProjectilesCount = 1;
                    break;
                    
                case 2:
                    _additionalProjectilesCount = 2;
                    break;

                case 3:
                    _additionalProjectilesCount = 3;
                    break;

                case 4:
                    _additionalProjectilesCount = 4;
                    break;

                case 5:
                    _additionalProjectilesCount = 5;
                    break;

                case 6:
                    _additionalProjectilesCount = 6;
                    break;

                case 7:
                    _additionalProjectilesCount = 7;
                    break;

                case 8:
                    _additionalProjectilesCount = 8;
                    break;

                default:
                    _additionalProjectilesCount = 8;
                    break;
            }

            GlobalBonuses.Instance.UpdateAdditionalProjectilesCount(_additionalProjectilesCount);
        }
    }
}