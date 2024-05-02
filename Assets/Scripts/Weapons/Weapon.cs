using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [HideInInspector] public GameManager.WeaponType weaponType;
    [HideInInspector] public float activateCD = -1;  // This shouldn't be using
    [HideInInspector] public int ammo = -1;
    [HideInInspector] public int maxAmmo = -1;

    [HideInInspector] public CharacterPlayer.WeaponSlots weaponSlot;

    public Vector3 direction = new(0,1,0);

    public virtual void activateWeapon()
    {
        ammo--;
    }

    private void Update()
    {
        if (activateCD > 0)
        {
            activateCD -= Time.deltaTime;
        }
        else if (activateCD <= 0)
        {
            activateWeapon();
        }

        // ammo check
        if (ammo <= 0)
        {
            CharacterPlayer.instance.OnChangingWeapon(weaponSlot, false);
        }
    }
}
