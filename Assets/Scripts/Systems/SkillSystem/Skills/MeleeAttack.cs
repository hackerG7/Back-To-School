using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MeleeAttack : Skill
{
    public float Distance = 50;
    public float Angle = 90;//The angle of the attack area
    public float Damage = 10;//the power of explosion
    public MeleeAttack(string skillID, string skillName, float cooldown)
    : base(skillID, skillName, cooldown)
    {
        // DerivedClass parameter types have to match base class types
        // Do additional work here otherwise you can leave it empty
    }
    public MeleeAttack SetDistance(float distance)
    {
        Distance = distance;
        return this;
    }
    public MeleeAttack SetAngle(float angle)
    {
        Angle = angle;
        return this;
    }
    public MeleeAttack SetDamage(float damage)
    {
        Damage = damage;
        return this;
    }

    public override void Run(Entity entity)
    {
        //Bullet builder
        BulletSystem.Instance.CreateBullet("MeleeAttack")//Create the bullet
        .DestroyAfterSeconds(0.1f)
        .OnDeath((b) => PhysicsExtension.Explosion(b.transform.position, 10f, 200))//when death, explosion
        .SetDamage(10)
        .Shoot(entity, 1200);//Shoot the bullet

        AudioManager.Instance.PlaySound("attack");


    }
}
