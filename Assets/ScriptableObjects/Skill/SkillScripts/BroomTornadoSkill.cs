using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/BroomTornadoSkill")]
public class BroomTornadoSkill : Skill
{

    public override void Run(Entity master)
    {//skill content
        BulletSystem.Instance.CreateBullet("Tornado")//Create the bullet
        .OnEntityCollision((b, e) => e.AddState(master, "Airborne", 2))
        .Shoot(master, 1000);//Shoot the bullet


    }
}
