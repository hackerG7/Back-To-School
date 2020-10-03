using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FireballSkill : Skill
{
    public float ShootForce { get; set; } = 500;
    public float Power = 50;//the power of explosion
    public override void Run(Entity entity)
    {
        //Bullet builder
        BulletSystem.Instance.CreateBullet("Fireball")//Create the bullet
        .OnDeath((b) => PhysicsExtension.Explosion(b.transform.position, 10f, 60f*Power))//when death, explosion
        .Shoot(entity, ShootForce);//Shoot the bullet


        Debug.Log($"running skill fireball!");
    }
}
