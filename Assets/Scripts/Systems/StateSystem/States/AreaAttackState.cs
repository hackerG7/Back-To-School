using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttackState : State
{
    public float AttackTimer = 0;

    //parameters
    public float AttackIntverval = 0.1f;//the cooldown between every attack
    public float Radius = 10f;
    public float Damage = 1;

    public AreaAttackState(string stateID, string stateName) : base(stateID, stateName)
    {
    }

    public AreaAttackState Initiate(float attackInterval = 0.1f, float radius = 10f, float damage = 1)
    {
        AttackIntverval = attackInterval;
        Radius = radius;
        Damage = damage;
        return this;
    }
    public override void Update(Entity master, Entity target)
    {
        if(AttackTimer < AttackIntverval)
        {
            AttackTimer += Time.deltaTime;
            return;//stop
        }
        //Attack
        PhysicsExtension.Explosion(target.transform.position, 5f, 30);
        Collider[] objects = UnityEngine.Physics.OverlapSphere(target.transform.position, Radius);
        foreach (Collider h in objects)
        {
            Entity e = h.GetComponent<Entity>();
            if (e == null) continue;
            if (e == target) continue;
            e.Health -= Damage;

        }
    }

}
