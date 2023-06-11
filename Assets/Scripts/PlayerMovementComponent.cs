using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5.0f;
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private Animator Animator;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    private Vector3 lookVector = new Vector3(1f, 0f, 0f);

    private Vector2 movement;
    private bool facingLeft = false;
    private bool isMoving = false;

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (Mathf.Abs(movement.x) + Mathf.Abs(movement.y) > 0.1f)
        {
            lookVector = new Vector3(movement.x, movement.y, lookVector.z).normalized;
        }

        Animator.SetFloat("Speed", GetMoveSpeed());

        if (movement.x < 0f && !facingLeft) FlipSprite();
        if (movement.x > 0f && facingLeft) FlipSprite();
    }

    private void FixedUpdate()
    {
        var moveDirection = new Vector3(movement.x, movement.y).normalized;
        Rigidbody.MovePosition(transform.position + moveDirection * MoveSpeed * Time.deltaTime);

        Debug.DrawLine(transform.position, transform.position + lookVector, Color.red);
    }

    private void FlipSprite()
    {
        SpriteRenderer.flipX = !SpriteRenderer.flipX;
        facingLeft = !facingLeft;
        lookVector = new Vector3(lookVector.x * -1f, lookVector.y, lookVector.z);
    }

    public Vector3 GetLookDirection()
    {
        return lookVector;
    }

    public float GetMoveSpeed()
    {
        return Mathf.Abs(Mathf.Abs(movement.x) >= Mathf.Abs(movement.y) ? movement.x * MoveSpeed : movement.y * MoveSpeed);
    }
}