using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAoeDamage : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _radialDamagePrefab;

    private List<string> _tagsToDamage;
    private float _damage = 5f;
    private float _damageFrequency = 1f;
    private float _lifeTime;
    private float _damageRadius;

    private GameObject _radialDamageInstance;

    public void Initialize(List<string> tagsToDamage, float damageRadius, float damage, float damageFrequency, float lifeTime)
    {
        _tagsToDamage = new List<string>(tagsToDamage);
        _lifeTime = lifeTime;
        _damage = damage;
        _damageFrequency = damageFrequency;
        _damageRadius = damageRadius;

        _radialDamageInstance = InstantiateRadialDamage();
        StartCoroutine(OnLifeTime());
    }

    private IEnumerator OnLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        _animator.SetBool("IsDeath", true);
        Destroy(_radialDamageInstance);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private GameObject InstantiateRadialDamage()
    {
        GameObject radialDamage = Instantiate(_radialDamagePrefab, transform.position, Quaternion.identity, transform);
        radialDamage.GetComponent<RadialDamage>().Initialize(_tagsToDamage, _damageRadius, _damage, _damageFrequency);
        return radialDamage;
    }
}