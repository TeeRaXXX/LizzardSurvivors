using UnityEngine;

public class SnailBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _snailToSpawn;
    [SerializeField] private FollowObjectComponent _followObjectComponent;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private EnemyType _snailToSpawnType;
    [SerializeField] private bool _followPlayer;

    private GameObject _otherSnail;
    private Transform _playerTransform;
    private bool _isSingle;

    public bool _isTriggered;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag(TagsHandler.GetPlayerTag()).transform;

        _isTriggered = false;
        _otherSnail = null;
        _isSingle = true;
    }

    private void Update()
    {
        if (_otherSnail == null)
        {
            FindPair();

            if (_followPlayer) _followObjectComponent.SetFollowObject(_playerTransform);
        }
        else _followObjectComponent.SetFollowObject(_otherSnail.transform);
    }

    private void FindPair()
    {
        var snails = GameObject.FindGameObjectsWithTag(transform.tag);

        foreach (var snail in snails)
        {
            if (snail == this.gameObject)
                continue;

            var snailComponent = snail.GetComponent<SnailBehavior>();

            if (snailComponent != null)
                if (snailComponent.IsSingle())
                {
                    snailComponent.SetPair(this.gameObject);
                    SetPair(snail);
                    _isSingle = false;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(transform.tag) && !_isTriggered)
        {
            SetTriggered(true);
            other.GetComponent<SnailBehavior>().SetTriggered(true);
            Destroy(other.gameObject);
            EnemiesSpawnHandler.Instance.SpawnEnemy(_snailToSpawnType, transform.position);
            Destroy(this.gameObject);
        }
    }

    public bool IsSingle() => _isSingle;

    public void SetTriggered(bool isTriggered) => _isTriggered = isTriggered;

    public void SetPair(GameObject pair)
    {
        _isSingle = false;
        _otherSnail = pair;
    }
}