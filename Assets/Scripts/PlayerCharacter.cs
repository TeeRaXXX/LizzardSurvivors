using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private SOCharacter _selectedCharacter;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private HealthComponent _PlayerHealthComponent;
    [SerializeField] private GameObject _damageDigitView;
    [SerializeField] private Transform _damageDigitViewPosition;

    private PlayerStats _playerStats;
    private PlayerInventory _playerInventory;

    private readonly UnityEvent<float, float, GameObject> _onHealthChanged = new UnityEvent<float, float, GameObject>();

    public void Initialize(SkillsSpawner skillsHandler)
    {
        _playerStats = new PlayerStats();
        _playerStats.InitStats(_selectedCharacter);

        _PlayerHealthComponent.InitHealth(
            _selectedCharacter.CharacterBaseStats.GetMaxHealth(),
            _selectedCharacter.CharacterBaseStats.GetArmor(),
            _onHealthChanged);

        _onHealthChanged.AddListener(OnHelthChangedEvent);

        _playerSpriteRenderer.sprite = _selectedCharacter.CharacterBaseSprite;

        _playerAnimator.runtimeAnimatorController = _selectedCharacter.CharacterAnimationController;

        _playerMovement.InitMovement(_selectedCharacter, _playerSpriteRenderer, _playerAnimator);

        _playerInventory = new PlayerInventory();
        _playerInventory.InitInventory(_selectedCharacter.BaseActiveSkill, skillsHandler, 0);
    }

    private void OnHelthChangedEvent(float newHealth, float oldHealth, GameObject damageSource)
    {
        EventManager.OnPlayerHealthChangedEvent(newHealth, _selectedCharacter.CharacterBaseStats.GetMaxHealth());

        if (oldHealth - newHealth <= 0)
        {
            var damageDigitView = Instantiate(_damageDigitView, _damageDigitViewPosition.position, Quaternion.identity, gameObject.transform);
            damageDigitView.GetComponent<DamageDigitView>().Initialize(oldHealth - newHealth);
        }

        if (newHealth <= 0)
        {
            OnDeath(damageSource);
        }
    }

    private void OnDeath(GameObject killer)
    {
        Debug.Log($"Character has been killed by {killer.name}");
        EventManager.OnGameOverEvent();
    }
}