using UnityEngine;
using System.Collections.Generic;

public class ProjectileMusic : MonoBehaviour
{
    [SerializeField] private float Damage = 25f;
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float MinGravity = -0.1f;
    [SerializeField] private float MaxGravity = 0.1f;
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private List<Sprite> Sprites;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    public void Lounch(Vector3 lounchVector)
    {
        int index = Random.Range(0, Sprites.Count);
        SpriteRenderer.sprite = Sprites[index];
        Rigidbody.gravityScale = Random.Range(MinGravity, MaxGravity);
        Rigidbody.AddForce(lounchVector * Speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null && other.tag != "Player")
        {
            other.GetComponent<HealthComponent>().ApplyDamage(Damage, this.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "DestroyVolume")
        {
            Destroy(this.gameObject);
        }
    }
}