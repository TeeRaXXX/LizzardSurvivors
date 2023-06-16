using System.Collections;
using UnityEngine;

public sealed class SkillMusic : ActiveSkill
{
    [SerializeField] private float _coolDown = 1f;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _launchPoint;

    private bool isAtacking = false;
    public new void Upgrade()
    {
        base.Upgrade();
        //
    }

    private IEnumerator AttackWithDelay()
    {
        isAtacking = true;
        Instantiate(_projectile, _launchPoint.position, _launchPoint.rotation);
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