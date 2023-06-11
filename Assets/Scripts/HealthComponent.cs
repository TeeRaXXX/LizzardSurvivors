using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float Health = 100f;

    private bool isDead = false;

    public bool ApplyDamage(float damage, Transform damageDealer = null)
    {
        if (isDead) return true;

        if (Health - damage <= 0)
        {
            isDead = true;
            Debug.Log($"{this.name} get {damage} damage from {damageDealer.name} and die!");
            OnDeath();
            return true;
        }    

        Health -= damage;
        Debug.Log($"{this.name} get {damage} damage from {damageDealer.name}. {Health} health left!");
        return false;
    }

    private void OnDeath()
    {
        Destroy(this.gameObject);
    }    

    public bool IsDead()
    {
        return isDead;
    }    

    public float GetHealth()
    {
        if (isDead) return 0f;
        return Health;
    }
}
