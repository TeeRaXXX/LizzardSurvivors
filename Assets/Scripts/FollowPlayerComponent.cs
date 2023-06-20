using UnityEngine;

public class FollowPlayerComponent : MonoBehaviour
{
    [SerializeField] private Transform _followObject;
    [SerializeField] private Rigidbody2D _rigidbody;

    private float _moveSpeed;
    private bool _isEnable;

    private void Awake()
    {
        _isEnable = false;
    }

    private void FixedUpdate()
    {
        if (_followObject != null && _isEnable)
        {
            var moveDirection = new Vector3(_followObject.position.x - transform.position.x,
                                            _followObject.position.y - transform.position.y,
                                            0f).normalized;
            _rigidbody.MovePosition(transform.position + moveDirection * _moveSpeed * Time.deltaTime);
        }
    }

    public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;

    public void SetFollowObject(Transform followObject)
    {
        _followObject = followObject;
    }

    public void SetActive(bool isEnable) => _isEnable = isEnable;
}