using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponDatabase : MonoBehaviour
{
    public static WeaponDatabase Instance;//Singleton
    public List<Weapon> WeaponList = new List<Weapon>();

    private void Awake()
    {
        Instance = this;//Singleton
    }

    public Weapon FindWeaponByID(string weaponID)
    {
        return WeaponList.FirstOrDefault(s => s.WeaponID == weaponID);
    }
}
