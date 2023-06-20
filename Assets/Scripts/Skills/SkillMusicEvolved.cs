using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class SkillMusicEvolved : ActiveSkill
{
    [SerializeField] private float _coolDown = 1f;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private int _projectileCount = 3;
    [SerializeField] private float _positionOffset = 1.5f;
    [SerializeField] private float _positionOffset2 = 0.1f;
    [SerializeField] private float _rotationSpeed = 4f;

    private bool isAtacking = false;
    private PlayerMovement _playerMovement;
    private float _projectileFrequency;

    public new void Upgrade()
    {
        base.Upgrade();
        //
    }

    private void Awake()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _projectileFrequency = 360f / _projectileCount / _rotationSpeed;
    }

    private IEnumerator AttackWithDelay()
    {
        isAtacking = true;
        Vector3 playerLook = _playerMovement.GetLookDirection();
        bool addOffset = false;

        for (int i = 0; i < _projectileCount; i++)
        {
            float x = _launchPoint.position.x + _positionOffset;
            if (addOffset)
                x = _launchPoint.position.x + _positionOffset + _positionOffset2;

            var projectile = Instantiate(_projectile, new Vector3(
                x,
                _launchPoint.position.y,
                0f),
                new Quaternion(0f, 0f, 0f, 1),
                _launchPoint.transform);

            projectile.GetComponent<ProjectileMusicEvolved>().Lounch(_rotationSpeed, _launchPoint.gameObject, _projectileCount);
            addOffset = !addOffset;
            yield return new WaitForSeconds(_projectileFrequency);
        }
        
        yield return new WaitForSeconds(_coolDown);
    }

    private void FixedUpdate()
    {
        if (!isAtacking)
        {
            StartCoroutine(AttackWithDelay());
        }
        _launchPoint.transform.Rotate(new Vector3(0f, 0f, _rotationSpeed) * Time.deltaTime);
    }
}