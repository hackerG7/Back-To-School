using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneState : State
{
    private float AttackTimer = 0;

    //parameters
    public float AttackIntverval = 0.1f;//the cooldown between every attack
    public float Radius = 10f;
    public float Damage = 1;


    public AirborneState(string stateID, string stateName) : base(stateID, stateName)
    {
    }

    public override void RunStart(Entity master, Entity target)
    {
    }
    public override void RunUpdate(Entity master, Entity target)
    {
        target.Rigidbody.AddForce(0, 15, 0);
    }

    public override void RunEnd(Entity master, Entity target)
    {
    }

}
