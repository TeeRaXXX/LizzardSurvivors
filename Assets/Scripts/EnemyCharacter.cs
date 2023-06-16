using UnityEngine;
using UnityEngine.Events;

public class EnemyCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _enemySpriteRenderer;
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private FollowPlayerComponent _followPlayerComponent;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private SOEnemy _enemyParams;

    private readonly UnityEvent<float, GameObject> _onHealthChanged = new UnityEvent<float, GameObject>();

    public void Awake()
    {
        _enemySpriteRenderer.sprite = _enemyParams.EnemyBaseSprite;
        _enemyAnimator.runtimeAnimatorController = _enemyParams.EnemyAnimationController;

        _followPlayerComponent.SetFollowObject(GameObject.FindGameObjectWithTag("Player").transform);

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
            Debug.Log($"Character {gameObject.name} has been killed by {damageSource.name}");
            EventManager.OnEnemyDiedEvent(_enemyParams.EnemyType);
            Destroy(this.gameObject);
        }
    }
}