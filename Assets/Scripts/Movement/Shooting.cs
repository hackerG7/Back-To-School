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
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletprefab, bulletspawn.position, bulletspawn.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            rb.AddRelativeForce(0, 0, shootingforce * Time.deltaTime,ForceMode.Impulse);
            
            
        }
    }
}
