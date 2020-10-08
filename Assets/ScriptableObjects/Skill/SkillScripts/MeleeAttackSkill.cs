using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Skill/MeleeAttackSkill")]
public class MeleeAttackSkill : Skill
{
    public float Distance = 50;
    public float Angle = 90;//The angle of the attack area
    public float Damage = 10;//the power of explosion
    public override void Run(Entity entity)
    {
        //Bullet builder
        BulletSystem.Instance.CreateBullet("MeleeAttack")//Create the bullet
        .DestroyAfterSeconds(0.1f)
        .OnDeath((b) => PhysicsExtension.Explosion(b.transform.position, 10f, 200))//when death, explosion
        .SetDamage(10)
        .Shoot(entity, 1200);//Shoot the bullet
        entity.RunWeaponAttackAnimation();
        AudioManager.Instance.PlaySound("attack");


    }
}
