using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private HealthComponent _PlayerHealthComponent;
    [SerializeField] private GameObject _damageDigitView;
    [SerializeField] private GameObject _playerCameraObject;
    [SerializeField] private Transform _damageDigitViewPosition;
    [SerializeField] private PlayerHealthView _playerHealthView;
    [SerializeField] private SkillsHolder _skillsHolder;
    [SerializeField] private PlayersDropPicker _dropPicker;
    [SerializeField] private CharacterPointer _characterPointer;
    [SerializeField] private float _smoothCameraSpeed = 0.25f;
    [SerializeField] private float _splitCameraScale = 0.95f;

    public static int PlayersCount { get; set; }
    public static FollowObject _followPlayerCamera;

    private FollowObject _generalPlayerCamera;
    private PlayerStats _playerStats;
    private PlayerInventory _playerInventory;
    private SOCharacter _selectedCharacter;
    private PlayerInputHandler _playerInput;
    private int _playerIndex;
    private Buffs _buffs;
    private Debuffs _debuffs;
    private Sprite _playerLogo;

    private readonly UnityEvent<float, float, GameObject> _onHealthChanged = new UnityEvent<float, float, GameObject>();

    public int PlayerIndex => _playerIndex;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerInventory PlayerInventory => _playerInventory;
    public Sprite PlayerLogo => _playerLogo;
    public SOCharacter Character => _selectedCharacter;

    public void Initialize(int playerIndex, SkillsSpawner skillsHandler, SOCharacter selectedCharacter, GameModes gameMode)
    {
        InitCamera(playerIndex, gameMode);

        if (PlayersCount > 1)
            _characterPointer.Initialize(selectedCharacter.CharacterLogo);

        _skillsHolder.Initialize(playerIndex);
        _dropPicker.Initialize(playerIndex);
        _playerIndex = playerIndex;
        _playerLogo = selectedCharacter.CharacterLogo;
        _selectedCharacter = selectedCharacter;
        _playerStats = new PlayerStats();
        _playerStats.InitStats(_selectedCharacter);
        _buffs = new Buffs();
        _debuffs = new Debuffs();

        _PlayerHealthComponent.InitHealth(
            _selectedCharacter.CharacterBaseStats.GetMaxHealth(),
            _selectedCharacter.CharacterBaseStats.GetArmor(),
            _onHealthChanged);

        _onHealthChanged.AddListener(OnHelthChangedEvent);

        _playerSpriteRenderer.sprite = _selectedCharacter.CharacterBaseSprite;

        _playerAnimator.runtimeAnimatorController = _selectedCharacter.CharacterAnimationController;

        _playerInventory = new PlayerInventory();
        _playerInventory.InitInventory(_selectedCharacter.BaseActiveSkill, skillsHandler, _playerIndex);

        EventManager.OnPlayerInitializedEvent(this);
    }

    private void InitCamera(int playerIndex, GameModes gameMode)
    {
        if (playerIndex == 0 && gameMode != GameModes.Battleroyal)
        {
            _generalPlayerCamera = Instantiate(_playerCameraObject,
                new Vector3(transform.position.x, transform.position.y, -30f), Quaternion.identity).GetComponent<FollowObject>();
            _generalPlayerCamera.SetSaveStartZValue(true);
            Rect camRect = new Rect();
            camRect.x = 0;
            camRect.y = 0;
            camRect.width = 1;
            camRect.height = 1;
            _generalPlayerCamera.gameObject.GetComponent<Camera>().rect = camRect;

            if (PlayersCount > 1)
            {
                _generalPlayerCamera.SetSmoothFollow(true);
                _generalPlayerCamera.SetSmoothSpeed(_smoothCameraSpeed);
            }

            _generalPlayerCamera.SetFollowObject(gameObject);
        }
        else if (gameMode == GameModes.Battleroyal)
        {
            _followPlayerCamera = Instantiate(_playerCameraObject,
                new Vector3(transform.position.x, transform.position.y, -30f), Quaternion.identity).GetComponent<FollowObject>();

            if (playerIndex > 0)
                _followPlayerCamera.gameObject.GetComponent<AudioListener>().enabled = false;

            _followPlayerCamera.SetSaveStartZValue(true);
            _followPlayerCamera.SetFollowObject(gameObject);

            Rect camRect = new Rect();

            if (PlayersCount == 2)
            {
                camRect.width = 0.5f;
                camRect.height = 1f;
                camRect.x = playerIndex * 0.5f;
            }
            if (PlayersCount == 3)
            {
                if (playerIndex == 0)
                {
                    camRect.width = 1f;
                    camRect.height = 0.5f;
                    camRect.y = 0.5f;
                }
                else
                {
                    camRect.width = 0.5f;
                    camRect.height = 0.5f;
                    switch (playerIndex)
                    {
                        case 1:
                            camRect.x = 0f;
                            break;
                        case 2:
                            camRect.x = 0.5f;
                            break;
                    }
                }
            }
            if (PlayersCount == 4)
            {
                camRect.width = 0.5f;
                camRect.height = 0.5f;
                switch (playerIndex)
                {
                    case 0:
                        camRect.x = 0f;
                        camRect.y = 0.5f;
                        break;
                    case 1:
                        camRect.x = 0.5f;
                        camRect.y = 0.5f;
                        break;
                    case 2:
                        camRect.x = 0f;
                        camRect.y = 0f;
                        break;
                    case 3:
                        camRect.x = 0.5f;
                        camRect.y = 0f;
                        break;
                }
            }

            _followPlayerCamera.gameObject.GetComponent<Camera>().rect = camRect;
        }
    }

    public void SetPlayerInput(PlayerInputHandler playerInput)
    {
        _playerInput = playerInput;
        _playerMovement.InitMovement(_selectedCharacter, _playerSpriteRenderer, _playerAnimator, _playerInput);
    }

    public void EnableCamera()
    {
        if (!_playerCameraObject.activeSelf)
            _playerCameraObject.SetActive(true);
    }

    private void OnHelthChangedEvent(float newHealth, float oldHealth, GameObject damageSource)
    {
        SoundManager.Instance.PlaySFX(oldHealth > newHealth ? "PlayerTakingDamage" : "PlayerTakingHeal");
        _playerHealthView.UpdateHealthBar(newHealth, _selectedCharacter.CharacterBaseStats.GetMaxHealth(), _playerIndex);

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
        PlayersCount--;
        Debug.Log($"Character has been killed by {killer.name}");
        EventManager.OnPlayerDiedEvent(_playerIndex);

        if (PlayersCount == 1)
        {
            _generalPlayerCamera.SetSmoothFollow(false);
            _generalPlayerCamera.SetFollowObject(GameObject.FindGameObjectsWithTag(TagsHandler.GetPlayerTag()).
                FirstOrDefault(o => o.GetComponent<PlayerCharacter>().PlayerIndex != PlayerIndex));
        }
        else if (PlayersCount > 1)
        {
            _generalPlayerCamera.SetFollowObject(GameObject.FindGameObjectsWithTag(TagsHandler.GetPlayerTag()).
                FirstOrDefault(o => o.GetComponent<PlayerCharacter>().PlayerIndex != PlayerIndex));
        }

        Destroy(gameObject);
    }
}