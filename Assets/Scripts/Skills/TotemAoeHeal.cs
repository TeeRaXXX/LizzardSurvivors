using System.Collections;
using UnityEngine;

public class TotemAoeHeal : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private float _lifeTime;

    private void Awake()
    {
        _lifeTime = 3f;

        StartCoroutine(OnLifeTime());
    }

    private IEnumerator OnLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        _animator.SetBool("IsDeath", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}