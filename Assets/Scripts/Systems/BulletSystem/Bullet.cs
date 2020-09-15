using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Entity Master;
    public Collider Collider;
    public Rigidbody RigidBody;

    public bool Piercing = false;

    public float AutoRemoveAfterSeconds = 1;

    public float Damage = 10;

    public void Shoot(Entity master, float force)
    {//Simple shooting with less arguments
        Shoot(master, master.transform.position, master.transform.rotation.eulerAngles, force);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="master">the master entity (the shooter)</param>
    /// <param name="position">the shoot position</param>
    /// <param name="shootDirection">the shooting direction</param>
    /// <param name="force">the shooting force</param>
    public void Shoot(Entity master, Vector3 position, Vector3 shootDirection, float force)
    {
        //Shooter 
        Master = master;

        //Transform
        gameObject.transform.position = position;
        gameObject.transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg*Mathf.Atan2(shootDirection.x, shootDirection.z), 0);//Circo please fix this line

        //Force adding
        RigidBody.velocity = Vector3.zero;//(0,0,0)
        Debug.Log("shootDirection.x: " + shootDirection.x);
        RigidBody.AddForce(shootDirection.x * force * Time.deltaTime, shootDirection.y * force * Time.deltaTime, shootDirection.z * force * Time.deltaTime, ForceMode.Impulse);

        //Auto Self Destroy
        Invoke("Remove", AutoRemoveAfterSeconds);
    }
    public void Remove()
    {
        MasterObjectPooler.Instance.Release(gameObject, "BulletPool");
    }

}
