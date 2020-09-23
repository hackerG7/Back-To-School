using JetBrains.Annotations;
using QFSW.MOP2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Entity Master;
    
    public Rigidbody RigidBody;
    public string ObjectPoolID;
    public List<Entity> HitList = new List<Entity>();

    public bool Piercing = false;    //passing through the enemy or not (eg. laser)

    public float AutoRemoveAfterSeconds = 1;

    public float Damage = 10;

    public Action<Bullet> DestroyCallback;//This is a function that would be called when the bullet destroy.
    public Action<Bullet, Entity> EntityCollisionCallback;//This is a function that would be called when the bullet collide with entity.
    public Bullet OnDeath(Action<Bullet> callback)
    {//Setting the destroy callback
        DestroyCallback = callback;
        return this;
    }
    public Bullet OnEntityCollision(Action<Bullet, Entity> callback)
    {//Setting the collision callback
        EntityCollisionCallback = callback;
        return this;
    }
    public Bullet SetDamage(float damage)
    {//Setting the destroy callback
        Damage = damage;
        return this;
    }
    public Bullet DestroyAfterSeconds(float seconds)
    {//Set the auto destroy seconds
        this.AutoRemoveAfterSeconds = seconds;
        return this;
    }

    #region Shoot
    public Bullet Shoot(Entity master, float force)
    {//Simple shooting with less arguments
        return Shoot(master, master.transform.position, master.transform.rotation, force);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="master">the master entity (the shooter)</param>
    /// <param name="position">the shoot position</param>
    /// <param name="shootDirection">the shooting direction</param>
    /// <param name="force">the shooting force</param>
    public Bullet Shoot(Entity master, Vector3 position, Vector3 shootDirection, float force)
    {
        //Shooter 
        Master = master;
        HitList = new List<Entity>();
        //Transform
        gameObject.transform.position = position;
        gameObject.transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan2(shootDirection.x, shootDirection.z), 0);

        //Force adding
        RigidBody.velocity = Vector3.zero;//(0,0,0)
        RigidBody.AddForce(shootDirection.x * force, shootDirection.y * force, shootDirection.z * force, ForceMode.Impulse);

        //Auto Self Destroy
        Invoke("Remove", AutoRemoveAfterSeconds);
        return this;
    }
    public Bullet Shoot(Entity master, Vector3 position, Quaternion shootDirection, float force)
    {
        //Shooter 
        Master = master;
        HitList = new List<Entity>();
        //Transform
        gameObject.transform.position = position;
        gameObject.transform.rotation = shootDirection;

        //Force adding
        RigidBody.velocity = Vector3.zero;//(0,0,0)
        RigidBody.AddForce(shootDirection * Vector3.forward * force);
        
        //Auto Self Destroy
        Invoke("Remove", AutoRemoveAfterSeconds);
        return this;
    }
    #endregion
    public void Remove()
    {
        if(DestroyCallback!=null)
            DestroyCallback(this);
        MasterObjectPooler.Instance.Release(gameObject, ObjectPoolID);
    }
    private void OnCollideWith(Collider collider)
    {
        Entity target = collider.gameObject.GetComponent<Entity>();
        if (target == null) return;
        if (target == Master) return;

        bool success = false;//successfully collide?
        if (!Piercing)
        {
            success = true;
            Remove();
        }
        else
        {
            if (!HitList.Contains(target))
            {
                success = true;
                HitList.Add(target);
            }
        }
        if (success)
        {
            //Apply collision event
            target.Health -= Damage;
            if(EntityCollisionCallback!=null)
                EntityCollisionCallback(this, target);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        OnCollideWith(other);
    }
    void OnCollisionEnter(Collision collision)
    {
        OnCollideWith(collision.collider);
    }
    public Entity GetMaster()
    {
        return Master;
    }

    public Bullet SetMaster(Entity master)
    {
        this.Master = master;
        return this;
    }

}
