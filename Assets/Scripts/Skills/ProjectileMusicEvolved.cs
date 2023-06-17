using UnityEngine;
using System.Collections.Generic;

public class ProjectileMusicEvolved : MonoBehaviour
{
    [SerializeField] private float Damage = 25f;
    [SerializeField] private float Speed = 2f;
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private List<Sprite> Sprites;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    private bool _isWorking = false;
    private float _rotationSpeed;

    public void Lounch(float rotationSpeed)
    {
        int index = Random.Range(0, Sprites.Count);
        SpriteRenderer.sprite = Sprites[index];
        _rotationSpeed = rotationSpeed;
        _isWorking = true;
    }

    private void Update()
    {
        if (_isWorking)
            transform.Rotate(new Vector3(0f, 0f, -_rotationSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null && other.tag != "Player")
        {
            other.GetComponent<HealthComponent>().ApplyDamage(Damage, this.gameObject);
        }
    }
}