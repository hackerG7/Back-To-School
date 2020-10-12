using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EasyButtons;
using UnityEditor;

[ExecuteInEditMode]
public class WeaponDatabase : SingletonMonoBehaviour<WeaponDatabase>
{
    public List<Weapon> WeaponList = new List<Weapon>();

    public void Initiate()
    {
        LoadAllWeapons();
    }
    [Button]
    public void LoadAllWeapons()
    {//This is the place holding all skills data
        WeaponList = Resources.FindObjectsOfTypeAll<Weapon>().ToList();
        EditorUtility.SetDirty(this);
    }
    public Weapon FindWeaponByID(string weaponID)
    {
        return WeaponList.FirstOrDefault(s => s.WeaponID == weaponID);
    }
}
