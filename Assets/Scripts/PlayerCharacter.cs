using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private SOCharacter _selectedCharacter;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private HealthComponent _PlayerHealthComponent;

    private PlayerStats _playerStats;
    private PlayerInventory _playerInventory;

    private readonly UnityEvent<float, GameObject> _onHealthChanged = new UnityEvent<float, GameObject>();

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

    private void OnHelthChangedEvent(float health, GameObject damageSource)
    {
        EventManager.OnPlayerHealthChangedEvent(health, _selectedCharacter.CharacterBaseStats.GetMaxHealth());

        if (health <= 0)
        {
            Debug.Log($"Character has been killed by {damageSource.name}");
        }
    }
}