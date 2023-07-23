using System.Collections.Generic;
using UnityEngine;

public class ProjectileTotem : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private List<string> _tagsToDamage;
    private float _damage;

    public void Initialize(List<string> tagsToDamage, float damage, float speed)
    {
        _tagsToDamage = tagsToDamage;
        _damage = damage;

        _rigidbody.AddForce(GetRandomEnemyPosition().normalized * speed);
    }

    private Vector3 GetRandomEnemyPosition()
    {
        foreach (var enemyTag in TagsHandler.GetEnemyTags())
        {
            var enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            if (enemies == null || enemies.Length == 0)
                continue;

            GameObject enemy = enemies[Random.Range(0, enemies.Length - 1)];

            if (enemy != null)
            {
                return enemy.transform.position;
            }
        }

        return new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null && _tagsToDamage.Contains(other.tag))
        {
            other.GetComponent<HealthComponent>().ApplyDamage(_damage, gameObject);
            Destroy(gameObject);
        }
    }
}