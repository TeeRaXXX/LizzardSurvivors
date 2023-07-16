using Unity.Burst;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _enemySpriteRenderer;
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private FollowObjectComponent _followPlayerComponent;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private SOEnemy _enemyParams;
    [SerializeField] private SpriteFlipper _spriteFlipper;
    [SerializeField] private GameObject _damageDigitView;
    [SerializeField] private Transform _damageDigitViewPosition;

    private readonly UnityEvent<float, float, GameObject> _onHealthChanged = new UnityEvent<float, float, GameObject>();

    public void Start()
    {
        _enemySpriteRenderer.sprite = _enemyParams.EnemyBaseSprite;
        _enemyAnimator.runtimeAnimatorController = _enemyParams.EnemyAnimationController;
        _spriteFlipper.Init(this.transform, _enemySpriteRenderer);

        _followPlayerComponent.SetMoveSpeed(_enemyParams.EnemyBaseStats.GetMoveSpeed());
        _followPlayerComponent.SetFollowObject(GameObject.FindGameObjectWithTag("Player").transform);
        _followPlayerComponent.SetActive(true);

        _healthComponent.InitHealth(
            _enemyParams.EnemyBaseStats.GetMaxHealth(),
            _enemyParams.EnemyBaseStats.GetArmor(),
            _onHealthChanged);

        _onHealthChanged.AddListener(OnHelthChangedEvent);

    }
    private void OnHelthChangedEvent(float newHealth, float oldHealth, GameObject damageSource)
    {
        var damageDigitView = Instantiate(_damageDigitView, _damageDigitViewPosition.position, Quaternion.identity);
        damageDigitView.GetComponent<DamageDigitView>().Initialize(oldHealth - newHealth);

        if (newHealth <= 0)
            OnDeath();
    }

    private void OnDeath()
    {
        Drop();
        EventManager.OnEnemyDiedEvent(_enemyParams.EnemyType);
        _onHealthChanged.RemoveAllListeners();
        Destroy(this.gameObject);
    }

    private void Drop()
    {
        if (_enemyParams.Drops.Count > 0)
            foreach (var drop in _enemyParams.Drops)
                if (Random.Range(0f, 1f) <= drop.DropChance)
                {
                    var spawnedDrop = Instantiate(drop.DropPrefab, transform.position, Quaternion.identity);

                    IDroppable droppable;

                    if (spawnedDrop.TryGetComponent<IDroppable>(out droppable))
                        droppable.Drop(_enemyParams);
                }
    }

    public EnemyStats GetStats() => _enemyParams.EnemyBaseStats;
}