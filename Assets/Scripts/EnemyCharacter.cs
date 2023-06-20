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

    private readonly UnityEvent<float, GameObject> _onHealthChanged = new UnityEvent<float, GameObject>();

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
    private void OnHelthChangedEvent(float health, GameObject damageSource)
    {
        if (health <= 0)
        {
            EventManager.OnEnemyDiedEvent(_enemyParams.EnemyType);
            Destroy(this.gameObject);
        }
    }

    public EnemyStats GetStats() => _enemyParams.EnemyBaseStats;
}