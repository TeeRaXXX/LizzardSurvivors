using System.Collections;
using UnityEngine;

public class SkillAutoHeaal : MonoBehaviour
{
    [SerializeField] private float _healAmount = 1f;
    [SerializeField] private float _healFrequency = 1f;

    private bool _isHeal = false;
    private HealthComponent _healComponent;

    private void Awake()
    {
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
}
