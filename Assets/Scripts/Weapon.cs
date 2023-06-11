using UnityEngine;
using System.Collections;

public enum WeaponType
{
    Projectile,
    HitScan,
    Melee,
    AOE
}

public class Weapon : MonoBehaviour
{
    protected WeaponType weaponType;

    protected virtual IEnumerator AttackWithDelay()
    {
        yield return new WaitForSeconds(1f);
    }
}