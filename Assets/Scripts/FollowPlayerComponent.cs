using UnityEngine;

public class FollowPlayerComponent : MonoBehaviour
{

    [SerializeField] private float MoveSpeed = 10f;
    [SerializeField] private Transform FollowObject;
    [SerializeField] private Rigidbody2D rigidbody;

    private void FixedUpdate()
    {
        var moveDirection = new Vector3(FollowObject.position.x - transform.position.x, FollowObject.position.y - transform.position.y, 0f).normalized;
        rigidbody.MovePosition(transform.position + moveDirection * MoveSpeed * Time.deltaTime);
    }
}
