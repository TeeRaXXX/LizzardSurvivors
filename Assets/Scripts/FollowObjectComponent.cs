using NastyDoll.Utils;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectComponent : MonoBehaviour
{
    [SerializeField] private Transform _followObject;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _moveSpeed;

    private BuffsHandler _buffsHandler;
    private DebuffsHandler _debuffsHandler;
    private bool _isEnable;

    private void Awake()
    {
        _isEnable = false;
    }

    public void Initialize(float moveSpeed, Transform followObject, BuffsHandler buffsHandler, DebuffsHandler debuffsHandler)
    {
        _buffsHandler = buffsHandler;
        _debuffsHandler = debuffsHandler;
        _moveSpeed = moveSpeed;
        _followObject = followObject;
        _isEnable = true;
    }

    public void SetFollowObject(Transform followObject) => _followObject = followObject;

    private void FixedUpdate()
    {
        if (_followObject != null && _isEnable)
        {
            var moveDirection = new Vector3(_followObject.position.x - transform.position.x,
                                            _followObject.position.y - transform.position.y,
                                            0f).normalized;

            float moveSpeed = _moveSpeed - (_moveSpeed * _debuffsHandler.GetMoveSpeedDebuff()) + 
                                           (_moveSpeed * _buffsHandler.GetMoveSpeedBuff());

            _rigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        }
        else if (_followObject == null)
        {
            var objectToFollow = UtilsClass.GetNearestObject(transform.position, new List<GameObject>(GameObject.FindGameObjectsWithTag(TagsHandler.GetPlayerTag())));
            SetFollowObject(objectToFollow.transform);
        }
    }
}