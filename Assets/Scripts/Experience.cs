using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour, IDroppable
{
    [SerializeField] private SpriteRenderer _experienceSprite;
    [SerializeField] private List<Sprite> _experienceSprites;
    [SerializeField] private List<float> _experienceMaxValue;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _bounceForce = 2f;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _bounceTime = 0.75f;
    [SerializeField] private float _speedMultiplier = 1.025f;

    private int _experienceCount;
    private bool _isTaken;
    private Transform _captorTransform;

    public void Drop(SOEnemy enemy)
    {
        _isTaken = false;
        _experienceCount = Random.Range((int)enemy.ExperienceMin, (int)enemy.ExperienceMax);

        for (int i = 0; i < _experienceMaxValue.Count - 1; i++)
        {
            if (_experienceMaxValue[i] > _experienceCount)
            {
                _experienceSprite.sprite = _experienceSprites[i];
                return;
            }
        }
        _experienceSprite.sprite = _experienceSprites[_experienceMaxValue.Count - 1];
    }

    public void OnTake()
    {
        PlayerLevel.Instance.GetExperience(_experienceCount);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_isTaken)
        {
            var moveDirection = new Vector3(transform.position.x - _captorTransform.position.x,
                                            transform.position.y - _captorTransform.position.y,
                                            0f).normalized;
            _rigidbody.MovePosition(transform.position + moveDirection * _speed * Time.deltaTime);

            _speed -= _speedMultiplier;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagsHandler.GetExperienceTakerTag()) && !_isTaken)
        {
            _captorTransform = other.transform;
            _isTaken = true;
        }
    }

    public float GetExperienceCount => _experienceCount;
}