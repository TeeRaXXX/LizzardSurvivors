using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject _objectToFollow = null;
    [SerializeField] private string _tagToFollow = "null";

    private void Awake()
    {
        if (!_tagToFollow.Equals(""))
            _objectToFollow = GameObject.FindGameObjectWithTag(_tagToFollow);
    }

    void Update()
    {
        if (_objectToFollow != null)
            transform.position = _objectToFollow.transform.position;
    }
}