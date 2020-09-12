using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Control
    public float movementspeed = 20f;
    Vector3 movement;

    //Add force
    public Rigidbody rb;
    

    //SpeedLock
    Vector3 speed;

    //maxspeed
    public float maxspeed = 10f;

    //Camera control   
    public Transform campos;    
    public Vector3 offset;

    //player rotation
    public Camera cam;
    float raylength;
    Vector3 pointtolook;


    void Start()
    {
        
    }



    
    void Update()
    {
        //Control
        movement.x = Input.GetAxisRaw("Horizontal") * movementspeed * Time.deltaTime;
        movement.z = Input.GetAxisRaw("Vertical") * movementspeed * Time.deltaTime;
        Debug.Log(raylength);


        

        //Locking Velocity to maxspeed
        speed.x = rb.velocity.x;
        speed.z = rb.velocity.z;

        speed.x = Mathf.Clamp(speed.x, -maxspeed, maxspeed);
        speed.z = Mathf.Clamp(speed.z, -maxspeed, maxspeed);

        if (rb.velocity.x > maxspeed || rb.velocity.z > maxspeed || rb.velocity.z < -maxspeed || rb.velocity.x < -maxspeed)
        {
            rb.velocity = new Vector3(speed.x, 0, speed.z);
        }

        //Zeroing speed
        if (movement.x == 0)
        {
            rb.velocity = new Vector3(0, 0, speed.z);
            speed.x = 0;
        }

        if (movement.z == 0)
        {
            rb.velocity = new Vector3(speed.x, 0, 0);
        }
        


        //camera control
        campos.position = rb.position + offset;


        //player rotation
        Ray camray = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundplane = new Plane(Vector3.up, rb.position);

        if (groundplane.Raycast(camray, out raylength))
        {
            pointtolook = camray.GetPoint(raylength);
            transform.LookAt(pointtolook);
        }
               
        Debug.DrawLine(camray.origin, pointtolook, Color.blue);




        

        
    }
    void FixedUpdate()
    {
        
        
        //Adding Force to GameObject
        rb.AddForce(movement.x, 0, movement.z, ForceMode.VelocityChange);
        
        
        
        
       

 
    }
}
