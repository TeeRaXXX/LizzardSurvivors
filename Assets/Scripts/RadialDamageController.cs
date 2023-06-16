using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialDamageController : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private float damageFrequency = 1f;

    [SerializeField] private List<Collider2D> objectsToDamage;
    private bool isDamageInProcess = false;

    private void Start()
    {
        objectsToDamage = new List<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (objectsToDamage.Count > 0 && !isDamageInProcess)
        {
            StartCoroutine(MakeDamage());
        }
    }

    private IEnumerator MakeDamage()
    {
        isDamageInProcess = true;

        for (int i = 0; i < objectsToDamage.Count; i++)
        {
            if (objectsToDamage[i] == null)
            {
                objectsToDamage.Remove(objectsToDamage[i]);
                continue;
            }

            var healthComponent = objectsToDamage[i].GetComponent<HealthComponent>();
            var isDead = healthComponent.ApplyDamage(damage, this.gameObject);
        }

        yield return new WaitForSeconds(damageFrequency);
        isDamageInProcess = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null)
        {
            objectsToDamage.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null)
        {
            objectsToDamage.Remove(other);
        }
    }
}