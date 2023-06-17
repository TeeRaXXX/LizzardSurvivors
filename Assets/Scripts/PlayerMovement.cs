using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody;

    private float _moveSpeed = 2.5f;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private Vector3 _lookVector = new Vector3(1f, 0f, 0f);
    private Vector2 _movement;
    private bool _facingLeft = false;

    public void InitMovement(SOCharacter character, SpriteRenderer spriteRenderer, Animator animator)
    {
        _moveSpeed = character.CharacterBaseStats.GetMoveSpeed();
        _spriteRenderer = spriteRenderer;
        _animator = animator;
        _animator.runtimeAnimatorController = character.CharacterAnimationController;
    }

    void Update()
    {
        _movement.x = InputManager.Instance.GetMovementVector().x;
        _movement.y = InputManager.Instance.GetMovementVector().y;

        if (Mathf.Abs(_movement.x) + Mathf.Abs(_movement.y) > 0.1f)
        {
            _lookVector = new Vector3(_movement.x, _movement.y, _lookVector.z).normalized;
        }

        _animator.SetFloat("Speed", GetMoveSpeed());

        if (_movement.x < 0f && !_facingLeft) FlipSprite();
        if (_movement.x > 0f && _facingLeft) FlipSprite();
    }

    private void FixedUpdate()
    {
        var moveDirection = new Vector3(_movement.x, _movement.y).normalized;
        Rigidbody.MovePosition(transform.position + moveDirection * _moveSpeed * Time.deltaTime);

        Debug.DrawLine(transform.position, transform.position + _lookVector, Color.red);
    }

    private void FlipSprite()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
        _facingLeft = !_facingLeft;
        _lookVector = new Vector3(_lookVector.x * -1f, _lookVector.y, _lookVector.z);
    }

    public Vector3 GetLookDirection()
    {
        return _lookVector;
    }

    public float GetMoveSpeed()
    {
        return Mathf.Abs(Mathf.Abs(_movement.x) >= Mathf.Abs(_movement.y) ? _movement.x * _moveSpeed : _movement.y * _moveSpeed);
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
}