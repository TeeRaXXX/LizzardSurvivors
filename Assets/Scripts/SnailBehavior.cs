using NastyDoll.Utils;
using UnityEngine;

public class SnailBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _snailToSpawn;
    [SerializeField] private FollowObjectComponent _followObjectComponent;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private EnemyType _snailToSpawnType;
    [SerializeField] private bool _followPlayer;
    [SerializeField] private float _spawnDistance;

    private GameObject _otherSnail;
    private Transform _playerTransform;
    private bool _isSingle;

    private void Awake()
    {
        _playerTransform = UtilsClass.GetNearestObject(transform.position, UtilsClass.FindObjectsWithTagsList(TagsHandler.GetPlayerTags())).transform;
        
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

    private void FixedUpdate()
    {
        if (_otherSnail != null)
        {
            if (Vector3.Distance(transform.position, _otherSnail.transform.position) <= _spawnDistance)
                Spawn();
        }
    }

    private void FindPair()
    {
        _isSingle = true;
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

    private void Spawn()
    {
        Destroy(_otherSnail);
        EnemiesSpawnHandler.Instance.SpawnEnemy(_snailToSpawnType, transform.position);
        Destroy(this.gameObject);
    }

    public bool IsSingle() => _isSingle;

    public void SetPair(GameObject pair)
    {
        _isSingle = false;
        _otherSnail = pair;
    }
}