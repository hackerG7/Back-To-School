using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    public GameObject bulletprefab;
    public Transform bulletspawn;
    public float shootingforce = 50000f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3 GetPlayerShootingDirection()
    {//Get the shooting direction of the player
        Vector3 endpoint = EntitySystem.Instance.MainPlayer.PlayerMovement.pointtolook;
        Vector3 startpoint = EntitySystem.Instance.MainPlayer.PlayerMovement.playertransform.position;
        Vector3 shootDirection = endpoint - startpoint;
        shootDirection = shootDirection.normalized;
        return shootDirection;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) { 

            BulletSystem.Instance.CreateBullet("TestBullet").Shoot(
                EntitySystem.Instance.MainPlayer, 
                bulletspawn.position,
                GetPlayerShootingDirection(), 
                shootingforce
                );//Initiate the bullet script;

            /*
            bulletObject.transform.position = bulletspawn.position;//setting bullet position
            bulletObject.transform.rotation = bulletspawn.rotation;//setting bullet rotation
            */
            //same function as Instantiate(bulletprefab, bulletspawn.position, bulletspawn.rotation);


            //Rigidbody rb = bulletObject.GetComponent<Rigidbody>();

            //rb.velocity = new Vector3(0, 0, 0);//resetting bullet velocity



            //rb.AddForce(shootdirection.x * shootingforce * Time.deltaTime, shootdirection.y * shootingforce * Time.deltaTime, shootdirection.z * shootingforce * Time.deltaTime, ForceMode.Impulse);





        }
    }
}
