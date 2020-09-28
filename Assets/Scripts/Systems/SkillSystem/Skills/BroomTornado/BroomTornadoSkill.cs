using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomTornadoSkill : Skill
{
    public BroomTornadoSkill(string skillID, string skillName, float cooldown)
    : base(skillID, skillName, cooldown)
    {
        // DerivedClass parameter types have to match base class types
        // Do additional work here otherwise you can leave it empty
    }

    public override void Run(Entity master)
    {//skill content
        BulletSystem.Instance.CreateBullet("Tornado")//Create the bullet
        .OnEntityCollision((b, e) => e.AddState(master, "Airborne", 2))
        .Shoot(master, 1000);//Shoot the bullet


    }
}
