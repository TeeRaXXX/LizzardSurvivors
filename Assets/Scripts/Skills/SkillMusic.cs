using System.Collections;
using UnityEngine;

public sealed class SkillMusic : ActiveSkill
{
    [SerializeField] private float _coolDown = 1f;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private float _spawnAngle = 10;
    [SerializeField] private int _projectileCount = 3;
    [SerializeField] private float _projectileFrequency = 0.35f;

    private bool isAtacking = false;
    private PlayerMovement _playerMovement;
    public new void Upgrade()
    {
        base.Upgrade();
        //
    }

    private void Awake()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private IEnumerator AttackWithDelay()
    {
        isAtacking = true;
        float angle = -_spawnAngle;
        Vector3 playerLook = _playerMovement.GetLookDirection();

        for (int i = 0; i < _projectileCount; i++)
        {
            float spawnVectorX = playerLook.x * Mathf.Cos(angle) - playerLook.y * Mathf.Sin(angle);
            float spawnVectorY = playerLook.x * Mathf.Sin(angle) + playerLook.y * Mathf.Cos(angle);
            angle += _spawnAngle;
            var projectile = Instantiate(_projectile, _launchPoint.position, _launchPoint.rotation);
            projectile.GetComponent<ProjectileMusic>().Lounch(new Vector3(spawnVectorX, spawnVectorY, 0).normalized);
            yield return new WaitForSeconds(_projectileFrequency);
        }
        
        yield return new WaitForSeconds(_coolDown);
        isAtacking = false;
    }

    private void FixedUpdate()
    {
        if (!isAtacking)
        {
            StartCoroutine(AttackWithDelay());
        }
    }
}