using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Thunderball : Skill
{
    public float ShootForce { get; set; } = 500;
    public Thunderball(string skillID, string skillName, float cooldown)
    : base(skillID, skillName, cooldown)
    {
        // DerivedClass parameter types have to match base class types
        // Do additional work here otherwise you can leave it empty
    }

    public override void Run(Entity entity)//entity is the skill caster
    {
        //Bullet builder
        BulletSystem.Instance.CreateBullet("Fireball")//Create the bullet
        .OnDeath((b) => PhysicsExtension.Explosion(b.transform.position, 10f, 3000f))//when death, explosion
        .Shoot(entity, ShootForce);//Shoot the bullet


        Debug.Log($"running skill fireball!");
    }
    public void Explosion(Bullet bullet)
    {

    }
}
