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
    public Bullet OnDeath(Action<Bullet> callback)
    {//Setting the destroy callback
        DestroyCallback = callback;
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

    void OnCollisionEnter(Collision collision)
    {
        Entity target = collision.gameObject.GetComponent<Entity>();
        if (target == null) return;
        if (target == Master) return;

        if (!Piercing)
        {
            target.Health -= Damage;
            Remove();
        }
        else
        {
            if (!HitList.Contains(target))
            {
                target.Health -= Damage;
                HitList.Add(target);
            }
        }
        foreach(ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        /*
        if (collision.relativeVelocity.magnitude > 2)
            audioSource.Play();*/

        
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
