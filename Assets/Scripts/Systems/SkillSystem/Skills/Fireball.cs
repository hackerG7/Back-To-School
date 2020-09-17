using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Fireball : Skill
{
    public float ShootForce { get; set; } = 500;
    public float Power = 50;//the power of explosion
    public Fireball(string skillID, string skillName, float cooldown)
    : base(skillID, skillName, cooldown)
    {
        // DerivedClass parameter types have to match base class types
        // Do additional work here otherwise you can leave it empty
    }
    public Fireball SetPower(float power)
    {
        Power = power;
        return this;
    }

    public override void Run(Entity entity)
    {
        //Bullet builder
        BulletSystem.Instance.CreateBullet("Fireball")//Create the bullet
        .OnDeath((b) => PhysicsExtension.Explosion(b.transform.position, 10f, 60f*Power))//when death, explosion
        .Shoot(entity, ShootForce);//Shoot the bullet


        Debug.Log($"running skill fireball!");
    }
}
