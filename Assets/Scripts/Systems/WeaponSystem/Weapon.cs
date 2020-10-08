using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Create Weapon")]
public class Weapon : ScriptableObject
{
    public string WeaponID;
    public string WeaponName;
    public string Description;
    public float Damage;
    public float AttackSpeed;
    public float Range;
    public AnimatorController AnimatorController;//The animator controller of the weapon



    public virtual void Run(Entity Master)
    {//When the player run the skill
        Debug.Log($"attacking with weapon for {Master}");
    }

}
