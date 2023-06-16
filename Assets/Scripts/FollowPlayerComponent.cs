using UnityEngine;

public class FollowPlayerComponent : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private Transform _followObject;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void FixedUpdate()
    {
        if (_followObject != null)
        {
            var moveDirection = new Vector3(_followObject.position.x - transform.position.x, _followObject.position.y - transform.position.y, 0f).normalized;
            _rigidbody.MovePosition(transform.position + moveDirection * _moveSpeed * Time.deltaTime);
        }
    }

    public void SetFollowObject(Transform followObject)
    {
        _followObject = followObject;
    }
}