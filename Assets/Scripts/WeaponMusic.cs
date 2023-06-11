using UnityEngine;
using System.Collections;

public class WeaponMusic : Weapon
{
    [SerializeField] private float CoolDown = 1f;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private Transform LounchPoint;

    private bool isAtacking = false;

    private void Awake()
    {
        weaponType = WeaponType.Projectile;
    }

    protected override IEnumerator AttackWithDelay()
    {
        isAtacking = true;
        Instantiate(Projectile, LounchPoint.position, LounchPoint.rotation);
        yield return new WaitForSeconds(CoolDown);
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