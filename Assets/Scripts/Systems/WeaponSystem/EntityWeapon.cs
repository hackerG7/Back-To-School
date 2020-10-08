using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityWeapon
{
    public Weapon Weapon;
    public bool WeaponReady = true;

    public float CurrentTime = 0;
    #region Constructor
    public EntityWeapon(Weapon weapon)
    {//Constructor
        Weapon = weapon;
    }
    public static EntityWeapon FromWeapon(Weapon Weapon)
    {//static constructor
        return new EntityWeapon(Weapon);
    }
    #endregion
    public void Run(Entity entity)
    {//Run the Weapon~
        if (WeaponReady)//if the cooldown loaded completely
        {
            if (Weapon == null) return;
            Weapon.Run(entity);//Run the Weapon
            WeaponReady = false;//Set the Weapon ready to false to prevent spamming Weapons.
            CurrentTime = 1 / Weapon.AttackSpeed;//Set the current time to cooldown, then start counting in void Update
        }

    }

    //The Entity Monobehaviour would run the update.
    public void Update()
    {
        if (CurrentTime > 0)
        {//Counting the cooldown. 
            CurrentTime -= Time.deltaTime;
        }
        else
        {
            if (!WeaponReady)
            {//Now the Weapon state turned into ready. 
                WeaponReady = true;
            }
        }
    }
}
