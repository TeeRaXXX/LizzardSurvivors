using UnityEngine;
using System.Collections.Generic;

public class ProjectileMusic : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _damage;
    private float _speed;
    private List<string> _tagsToDamage;

    public void Lounch(Vector3 lounchVector, float damage, float speed, List<string> tagsToDamage)
    {
        _damage = damage;
        _speed = speed;
        _tagsToDamage = new List<string>(tagsToDamage);
        int index = Random.Range(0, _sprites.Count);
        _spriteRenderer.sprite = _sprites[index];
        _rigidbody.AddForce(lounchVector * _speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_tagsToDamage.Contains(other.tag))
        {
            other.GetComponent<HealthComponent>().ApplyDamage(_damage, this.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == TagsHandler.GetDestroyVolumeTag())
        {
            Destroy(gameObject);
        }
    }
}