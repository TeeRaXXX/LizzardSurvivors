using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SkillAutoHeaal : MonoBehaviour, IUpgradable
{
    [SerializeField] private float _healAmount = 1f;
    [SerializeField] private float _healFrequency = 1f;

    private bool _isHeal = false;
    private HealthComponent _healComponent;
    private int _maxLevel;
    private int _currentLevel;

    private void Awake()
    {
        _currentLevel = 1;
        _maxLevel = 8;
        _healComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
        _isHeal = false;
    }

    private void Update()
    {
        if (!_isHeal)
        {
            StartCoroutine(Heal());
        }
    }

    private IEnumerator Heal()
    {
        _isHeal = true;
        _healComponent.ApplyHeal(_healAmount, this.gameObject);
        yield return new WaitForSeconds(_healFrequency);
        _isHeal = false;
    }

    public int GetMaxLevel() => _maxLevel;

    public void Upgrade()
    {
        if (_currentLevel < _maxLevel)
        {
            _currentLevel++;
            switch (_currentLevel)
            {
                case 2:
                    _healAmount = 1.5f;
                    break;

                case 3:
                    _healAmount = 2f;
                    break;

                case 4:
                    _healAmount = 2.5f;
                    break;

                case 5:
                    _healAmount = 3f;
                    break;

                case 6:
                    _healAmount = 3.5f;
                    break;

                case 7:
                    _healAmount = 4f;
                    break;

                case 8:
                    _healAmount = 4.5f;
                    break;
            }
        }
    }
}