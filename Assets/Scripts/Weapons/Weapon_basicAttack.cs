using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_BasicAttack : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    private void Start()
    {
        weaponType = GameManager.WeaponType.Basic;
        activateCD = 3.0f;
        ammo = 6;
        maxAmmo = 6;
        // Direction is set in the inspector. 
    }

    public override void activateWeapon()
    {
        var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().__init__(this.transform.position, direction);
        activateCD = 3.0f;
        ammo--;
    }
}
