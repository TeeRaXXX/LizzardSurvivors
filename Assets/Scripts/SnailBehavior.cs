using UnityEngine;

public class SnailBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _snailToSpawn;
    [SerializeField] private string _snailTag;
    [SerializeField] bool _followPlayer;

    private Rigidbody2D _rigidbody;
    private GameObject _otherSnail;
    private FollowPlayerComponent _followPlayerComponent;
    private float _moveSpeed;
    private bool _isSingle;

    public bool _isTriggered;

    private void Awake()
    {
        _isTriggered = false;
        _isSingle = true;
        _otherSnail = null;
        _followPlayerComponent = GetComponent<FollowPlayerComponent>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveSpeed = GetComponent<EnemyCharacter>().GetStats().GetMoveSpeed();
    }

    private void Update()
    {
        if (_otherSnail == null)
        {
            FindPair();

            if (_followPlayer)
                _followPlayerComponent.SetActive(true);
        }
        else
        {
            _followPlayerComponent.SetActive(false);
            MoveToOtherSnail();
        }
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
    
    private void MoveToOtherSnail()
    {
        if (_otherSnail == null)
            _isSingle = true;

        var moveDirection = new Vector3(_otherSnail.transform.position.x - transform.position.x,
                                            _otherSnail.transform.position.y - transform.position.y,
                                            0f).normalized;
        _rigidbody.MovePosition(transform.position + moveDirection * _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(transform.tag) && !_isTriggered)
        {
            _isTriggered = true;
            other.GetComponent<SnailBehavior>()._isTriggered = true;
            Destroy(other.gameObject);
            Instantiate(_snailToSpawn, transform.position, new Quaternion());
            Destroy(this.gameObject);
        }
    }

    public bool IsSingle() => _isSingle;
    public void SetPair(GameObject pair)
    {
        _isSingle = false;
        _otherSnail = pair;
    }
}