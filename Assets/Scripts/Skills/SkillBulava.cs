using NastyDoll.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillBulava : MonoBehaviour, IUpgradable
{
    [SerializeField] private GameObject _bulavaPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _spawnOffset;

    private int _maxLevel;
    private int _currentLevel;
    private float _coolDown;
    private float _damage;
    private bool _isReadyToWork;
    private List<string> _tagsToDamage;
    private PlayerCharacter _playerCharacter;

    public void Initialize(int playerIndex)
    {
        _isReadyToWork = false;
        _playerCharacter = UtilsClass.FindObjectsWithTagsList(TagsHandler.GetPlayerTags()).
            FirstOrDefault(p => p.GetComponent<PlayerCharacter>().PlayerIndex == playerIndex).GetComponent<PlayerCharacter>();
        _currentLevel = 1;
        _damage = 25f;
        _maxLevel = 8;
        _coolDown = 1f;
        _tagsToDamage = new List<string>(UtilsClass.GetPlayerCharacter(playerIndex).TagsToDamage);
        _isReadyToWork = true;
    }

    private void Update()
    {
        if (_isReadyToWork)
        {
            StartCoroutine(SpawnBulava(_coolDown));
        }   
    }

    private IEnumerator SpawnBulava(float cooldown)
    {
        _isReadyToWork = false;

        _spawnPosition.rotation = Quaternion.Euler(0f, 0f, UtilsClass.GetAngleFromVector(_playerCharacter.PlayerMovement.GetLookDirection()));

        Bulava bulava = Instantiate(_bulavaPrefab,
                                    new Vector3(_spawnPosition.position.x + _spawnOffset, _spawnPosition.position.y, 0f),
                                    Quaternion.identity,
                                    _spawnPosition).GetComponent<Bulava>();
        bulava.Initialize(_tagsToDamage, _damage, _spawnOffset);

        yield return new WaitForSeconds(cooldown);
        _isReadyToWork = true;
    }

    public int GetCurrentLevel() => _currentLevel;

    public int GetMaxLevel() => _maxLevel;

    public void Upgrade(bool isNewLevel)
    {
        if (isNewLevel)
            _currentLevel++;

        if (_currentLevel <= _maxLevel)
        {
            switch (_currentLevel)
            {
                case 1:
                    _damage = 25f;
                    _spawnPosition.localScale = new Vector3 (1f, 1f, 1f);
                    break;

                case 2:
                    _damage = 25f;
                    _spawnPosition.localScale = new Vector3 (1.3f, 1.3f, 1f);
                    break;

                case 3:
                    _damage = 45f;
                    _spawnPosition.localScale = new Vector3(1.3f, 1.3f, 1f);
                    break;

                case 4:
                    _damage = 45f;
                    _spawnPosition.localScale = new Vector3(1.6f, 1.6f, 1f);
                    break;

                case 5:
                    _damage = 65f;
                    _spawnPosition.localScale = new Vector3(1.6f, 1.6f, 1f);
                    break;

                case 6:
                    _damage = 65f;
                    _spawnPosition.localScale = new Vector3(1.9f, 1.9f, 1f);
                    break;

                case 7:
                    _damage = 85f;
                    _spawnPosition.localScale = new Vector3(1.9f, 1.9f, 1f);
                    break;

                case 8:
                    _damage = 85f;
                    _spawnPosition.localScale = new Vector3(2.2f, 2.2f, 1f);
                    break;

                default:
                    _damage = 85f;
                    _spawnPosition.localScale = new Vector3(2.2f, 2.2f, 1f);
                    break;
            }

        }
    }
}