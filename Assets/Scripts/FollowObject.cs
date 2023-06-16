using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject _objectToFollow = null;  

    void Update()
    {
        if (_objectToFollow != null)
            transform.position = _objectToFollow.transform.position;
    }
}