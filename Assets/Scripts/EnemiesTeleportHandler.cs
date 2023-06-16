using UnityEngine;

public class EnemiesTeleportHandler : MonoBehaviour
{
    [SerializeField] private Collider2D _colliderToTeleport;
    [SerializeField] private bool _isHorizontal;
    [SerializeField] private float _offset = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            float offset = _offset;

            if (_isHorizontal)
            {
                if (transform.position.x < 0f)
                    offset = _offset * -1;
                other.transform.position = new Vector3(_colliderToTeleport.bounds.center.x + offset, other.transform.position.y, 0f);
            }
            else
            {
                if (transform.position.y < 0f)
                    offset = _offset * -1;
                other.transform.position = new Vector3(other.transform.position.x, _colliderToTeleport.bounds.center.y + offset, 0f);
            }
        }
    }
}