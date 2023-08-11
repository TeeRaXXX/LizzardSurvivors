using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RadialDamage _radialDamage;
    private float _lifeTime;

    private GameObject _radialDamageInstance;

    public void Initialize(List<string> tagsToDamage, float damageRadius, float damage, float damageFrequency, float lifeTime)
    {
        _lifeTime = lifeTime;
        transform.localScale *= damageRadius;
        _radialDamage.Initialize(new List<string>(tagsToDamage), damageRadius, damage, damageFrequency);

        StartCoroutine(OnLifeTime());
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    private IEnumerator OnLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        _animator.SetBool("IsDeath", true);
        Destroy(_radialDamageInstance);
    }
}