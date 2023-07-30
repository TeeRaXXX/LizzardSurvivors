using System.Collections.Generic;
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
    [SerializeField] private RadialDamage _radialDamage;
    [SerializeField] private BuffsHandler _buffsHandler;
    [SerializeField] private DebuffsHandler _debuffsHandler;

    private Buffs _buffs;
    private Debuffs _debuffs;

    private readonly UnityEvent<float, float, GameObject> _onHealthChanged = new UnityEvent<float, float, GameObject>();

    public void Start()
    {
        _enemySpriteRenderer.sprite = _enemyParams.EnemyBaseSprite;
        _enemyAnimator.runtimeAnimatorController = _enemyParams.EnemyAnimationController;
        _spriteFlipper.Init(this.transform, _enemySpriteRenderer);

        _buffs = new Buffs();
        _debuffs = new Debuffs();
        _buffsHandler.Initialize(_buffs);
        _debuffsHandler.Initialize(_debuffs);

        _followPlayerComponent.Initialize(_enemyParams.EnemyBaseStats.GetMoveSpeed(),
                                          GameObject.FindGameObjectWithTag(TagsHandler.GetPlayerTag()).transform,
                                          _buffsHandler,
                                          _debuffsHandler);

        if (_radialDamage != null)
        {
            _radialDamage.Initialize(new List<string>() { TagsHandler.GetPlayerTag() },
                                     _enemyParams.EnemyBaseStats.GetAttackRadius(),
                                     _enemyParams.EnemyBaseStats.GetDamage(),
                                     _enemyParams.EnemyBaseStats.GetAttackSpeed());
        }

        _healthComponent.InitHealth(
            _enemyParams.EnemyBaseStats.GetMaxHealth(),
            _enemyParams.EnemyBaseStats.GetArmor(),
            _onHealthChanged);

        _onHealthChanged.AddListener(OnHelthChangedEvent);

    }

    public void AddBuff(IBuff buff) => _buffs.AddBuff(buff);
    public void RemoveBuff(IBuff buff) => _buffs.RemoveBuff(buff);

    public void AddDebuff(IDebuff debuff) => _debuffs.AddDebuff(debuff);
    public void RemoveDebuff(IDebuff debuff) => _debuffs.RemoveDebuff(debuff);

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