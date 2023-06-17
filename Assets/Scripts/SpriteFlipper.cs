using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    private SpriteRenderer _spriteRenderer;

    private float oldPosition;
    private bool movingRight = false;

    public void Init(Transform transform, SpriteRenderer spriteRenderer)
    {
        _transform = transform;
        _spriteRenderer = spriteRenderer;
    }

    private void LateUpdate()
    {
        oldPosition = transform.position.x;
    }

    private void Update()
    {
        if (_transform.position.x > oldPosition && movingRight)
        {
            movingRight = !movingRight;
            FlipSprite();
        }
        if (_transform.position.x < oldPosition && !movingRight)
        {
            movingRight = !movingRight;
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}