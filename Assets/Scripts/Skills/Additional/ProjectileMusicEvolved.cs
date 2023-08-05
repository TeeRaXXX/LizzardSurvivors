using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ProjectileMusicEvolved : MonoBehaviour
{
    [SerializeField] private float Damage = 25f;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private List<Sprite> Sprites;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private GameObject _additionalObject;
    [SerializeField] private float _changeSpriteTime = 3f;

    private bool _isRotating = false;
    private bool _isChangingColor = false;
    private float _rotationSpeed;

    public void Lounch(float rotationSpeed, GameObject pivot, int projectilesCount)
    {
        int index = UnityEngine.Random.Range(0, Sprites.Count);
        SpriteRenderer.sprite = Sprites[index];
        _rotationSpeed = rotationSpeed;
        _isRotating = true;
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
        int index = UnityEngine.Random.Range(0, Sprites.Count);
        SpriteRenderer.sprite = Sprites[index];
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
        if (other.GetComponent<HealthComponent>() != null && other.tag != "Player")
        {
            other.GetComponent<HealthComponent>().ApplyDamage(Damage, this.gameObject);
        }
    }
}