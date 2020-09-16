using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    public static BulletSystem Instance;//Singleton

    private void Awake()
    {
        Instance = this;//Singleton
    }
    public Bullet CreateBullet(string objectPoolID)
    {
        GameObject bulletObject = MasterObjectPooler.Instance.GetObject(objectPoolID);//getting object from the pool

        if (bulletObject == null)
            Debug.LogError($"Cannot find bullet object from pool: {objectPoolID}");
        
        Bullet bullet = bulletObject.GetComponent<Bullet>();//Get the Bullet script;
        bullet.ObjectPoolID = objectPoolID;//Setting the bullet object pool ID to release it later


        if (bullet == null)
            Debug.LogError($"Cannot find bullet script in prefab, bullet object pool ID: {objectPoolID}");
        
        return bullet;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
