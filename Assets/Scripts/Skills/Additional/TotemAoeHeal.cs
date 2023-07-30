using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAoeHeal : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _radialHealPrefab;

    private List<string> _tagsToHeal;
    private float _heal = 5f;
    private float _healFrequency = 1f;
    private float _lifeTime;
    private float _healRadius;

    private GameObject _radialHealInstance;

    public void Initialize(List<string> tagsToHeal, float healRadius, float heal, float healFrequency, float lifeTime)
    {
        _tagsToHeal = new List<string>(tagsToHeal);
        _lifeTime = lifeTime;
        _heal = heal;
        _healFrequency = healFrequency;
        _healRadius = healRadius;

        _radialHealInstance = InstantiateRadialHeal();
        StartCoroutine(OnLifeTime());
    }

    private IEnumerator OnLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        _animator.SetBool("IsDeath", true);
        Destroy(_radialHealInstance);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private GameObject InstantiateRadialHeal()
    {
        GameObject radialHeal = Instantiate(_radialHealPrefab, transform.position, Quaternion.identity, transform);
        radialHeal.GetComponent<RadialHeal>().Initialize(_tagsToHeal, _healRadius, _heal, _healFrequency);
        return radialHeal;
    }
}