using UnityEngine;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float Damage = 25f;
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float MinGravity = -0.1f;
    [SerializeField] private float MaxGravity = 0.1f;
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private List<Sprite> Sprites;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private PlayerMovementComponent PlayerMovement;

    void Start()
    {
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementComponent>();

        int index = Random.Range(0, Sprites.Count);
        SpriteRenderer.sprite = Sprites[index];
        Rigidbody.gravityScale = Random.Range(MinGravity, MaxGravity);
        Rigidbody.AddForce(PlayerMovement.GetLookDirection() * Speed * (PlayerMovement.GetMoveSpeed() + 1f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null && other.tag != "Player")
        {
            other.GetComponent<HealthComponent>().ApplyDamage(Damage, this.transform);
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