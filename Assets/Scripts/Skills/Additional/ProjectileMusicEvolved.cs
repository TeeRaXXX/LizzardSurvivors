using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ProjectileMusicEvolved : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private GameObject _additionalObject;
    [SerializeField] private float _changeSpriteTime = 3f;

    private bool _isRotating = false;
    private bool _isChangingColor = false;
    private float _rotationSpeed;
    private float _damage = 25f;
    private List<string> _tagsToDamage;
    private GameObject _pivot;

    public void Lounch(float rotationSpeed, GameObject pivot, int projectilesCount, List<string> tagsToDamage, float damage)
    {
        int index = Random.Range(0, _sprites.Count);
        _damage = damage;
        _tagsToDamage = new List<string>(tagsToDamage);
        SpriteRenderer.sprite = _sprites[index];
        _rotationSpeed = rotationSpeed;
        _isRotating = true;
        _pivot = pivot;
        StartCoroutine(StartWithDelay());
    }

    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(360f / _rotationSpeed);
    }

    private IEnumerator ChangeSpriteWithDelay()
    {
        _isChangingColor = true;
        yield return new WaitForSeconds(_changeSpriteTime);
        int index = UnityEngine.Random.Range(0, _sprites.Count);
        SpriteRenderer.sprite = _sprites[index];
        _isChangingColor = false;
    }

    private void Update()
    {
        if (_isRotating)
        {
            transform.Rotate(new Vector3(0f, 0f, -_rotationSpeed) * Time.deltaTime);

            if (!_isChangingColor)
                StartCoroutine(ChangeSpriteWithDelay());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>() != null && _tagsToDamage.Contains(other.tag))
        {
            other.GetComponent<HealthComponent>().ApplyDamage(_damage, _pivot);
        }
    }
}