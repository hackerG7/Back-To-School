using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
    public GameObject bulletprefab;
    public Transform bulletspawn;
    public float shootingforce = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) { 

            GameObject bulletObject = MasterObjectPooler.Instance.GetObject("BulletPool");//getting object from the pool
            Bullet bullet = bulletObject.GetComponent<Bullet>();//Get the Bullet script;
            bullet.Initiate();//Initiate the bullet script;
            bulletObject.transform.position = bulletspawn.position;//setting bullet position
            bulletObject.transform.rotation = bulletspawn.rotation;//setting bullet rotation

            //same function as Instantiate(bulletprefab, bulletspawn.position, bulletspawn.rotation);


            Rigidbody rb = bulletObject.GetComponent<Rigidbody>();

            rb.velocity = new Vector3(0, 0, 0);//resetting bullet velocity
            rb.AddRelativeForce(0, 0, shootingforce * Time.deltaTime,ForceMode.Impulse);//adding bullet veloci



        }
    }
}
