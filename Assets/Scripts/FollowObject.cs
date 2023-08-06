using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject _objectToFollow = null;
    [SerializeField] private string _tagToFollow = "null";
    private bool _smoothFollow;
    private bool _saveStartZValue;
    private float _smoothSpeed;

    public void SetSmoothFollow(bool isSmoothFollow) => _smoothFollow = isSmoothFollow;
    public void SetSaveStartZValue(bool saveStartZValue) => _saveStartZValue = saveStartZValue;
    public void SetSmoothSpeed(float smoothSpeed) => _smoothSpeed = smoothSpeed;

    private void Awake()
    {
        _saveStartZValue = false;
        _smoothFollow = false;
        _smoothSpeed = 0.25f;
        if (!_tagToFollow.Equals(""))
            _objectToFollow = GameObject.FindGameObjectWithTag(_tagToFollow);
        Debug.Log(transform.position.ToString());
    }

    private void LateUpdate()
    {
        if (_smoothFollow && _objectToFollow != null) 
        {
            Vector3 desiredPosition = _saveStartZValue ? new Vector3(_objectToFollow.transform.position.x,
                _objectToFollow.transform.position.y, transform.position.z) : _objectToFollow.transform.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    private void Update()
    {
        if (_objectToFollow != null && !_smoothFollow)
            transform.position = _saveStartZValue ? new Vector3(_objectToFollow.transform.position.x,
                _objectToFollow.transform.position.y, transform.position.z) : _objectToFollow.transform.position;
    }

    public void SetFollowObject(GameObject objectToFollow)
    {
        if (objectToFollow != null)
            _objectToFollow = objectToFollow;
    }
}