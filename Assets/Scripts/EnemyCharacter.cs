using NastyDoll.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _enemySpriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;
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
    [SerializeField] private List<GameObject> _behaviors;
    [SerializeField] private EnemyTakingDamageEffect _enemyTakingDamageEffect;

    private Buffs _buffs;
    private Debuffs _debuffs;
    private UnityEvent<float, float, GameObject> _onHealthChanged;

    public Animator Animator => _enemyAnimator;
    public FollowObjectComponent followObjectComponent => _followPlayerComponent;

    public void Start()
    {
        _enemySpriteRenderer.sprite = _enemyParams.EnemyBaseSprite;
        _enemyAnimator.runtimeAnimatorController = _enemyParams.EnemyAnimationController;
        _spriteFlipper.Init(transform, _enemySpriteRenderer);

        _buffs = new Buffs();
        _debuffs = new Debuffs();
        _buffsHandler.Initialize(_buffs);
        _debuffsHandler.Initialize(_debuffs);

        var objectToFollow = UtilsClass.GetNearestObject(transform.position, new List<GameObject>(UtilsClass.FindObjectsWithTagsList(TagsHandler.GetPlayerTags())));
        _followPlayerComponent.Initialize(_enemyParams.EnemyBaseStats.GetMoveSpeed(),
                                          objectToFollow.transform,
                                          _buffsHandler,
                                          _debuffsHandler);

        if (_radialDamage != null)
        {
            _radialDamage.Initialize(TagsHandler.GetPlayerTags(),
                                     _enemyParams.EnemyBaseStats.GetAttackRadius(),
                                     _enemyParams.EnemyBaseStats.GetDamage(),
                                     _enemyParams.EnemyBaseStats.GetAttackSpeed());
        }

        _onHealthChanged = new UnityEvent<float, float, GameObject>();

        _healthComponent.InitHealth(
            _enemyParams.EnemyBaseStats.GetMaxHealth(),
            _enemyParams.EnemyBaseStats.GetArmor(),
            _onHealthChanged);

        _onHealthChanged.AddListener(OnHelthChangedEvent);

        _enemyTakingDamageEffect.Initialize(_enemySpriteRenderer, _onHealthChanged);

        foreach (var behavior in _behaviors)
            behavior.GetComponent<IEnemyBehavior>().Initialize(this);
    }

    public void AddBuff(IBuff buff) => _buffs.AddBuff(buff);
    public void RemoveBuff(IBuff buff) => _buffs.RemoveBuff(buff);

    public void AddDebuff(IDebuff debuff) => _debuffs.AddDebuff(debuff);
    public void RemoveDebuff(IDebuff debuff) => _debuffs.RemoveDebuff(debuff);

    private void OnHelthChangedEvent(float newHealth, float oldHealth, GameObject damageSource)
    {
        if (oldHealth > newHealth)
        {
            SoundManager.Instance.PlaySFX("EnemyTakingDamage");
            if (oldHealth - newHealth > 20f)
                _rigidbody.AddForce(-damageSource.transform.position.normalized * Mathf.Clamp(20f * (oldHealth - newHealth), 0f, 700f));
        }
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
        {
            var dropPositions = UtilsClass.GetRadialPoints(_enemyParams.Drops.Count, 0.3f);

            for (int i = 0; i < _enemyParams.Drops.Count; i++)
                if (Random.Range(0f, 1f) <= _enemyParams.Drops[i].DropChance)
                {
                    var spawnedDrop = Instantiate(_enemyParams.Drops[i].DropPrefab, transform.position + dropPositions[i], Quaternion.identity);

                    IDroppable droppable;

                    if (spawnedDrop.TryGetComponent<IDroppable>(out droppable))
                        droppable.Drop(_enemyParams);
                }
        }
    }

    public EnemyStats GetStats() => _enemyParams.EnemyBaseStats;
}