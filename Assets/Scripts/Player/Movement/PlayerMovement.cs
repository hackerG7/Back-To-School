using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Control
    public float movementspeed = 20f;
    Vector3 movement;
    public Transform playertransform;

    //Add force
    public Rigidbody rb;
    

    //SpeedLock
    Vector3 speed;

    //maxspeed
    public float maxspeed = 10f;

    

    


    void Start()
    {
        
    }



    
    void Update()
    {
        //Control
        movement.x = Input.GetAxisRaw("Horizontal") * movementspeed * Time.deltaTime;
        movement.z = Input.GetAxisRaw("Vertical") * movementspeed * Time.deltaTime;
        //Debug.Log(raylength);


        //Locking Velocity to maxspeed
        speed.x = rb.velocity.x;
        speed.z = rb.velocity.z;

        speed.x = Mathf.Clamp(speed.x, -maxspeed, maxspeed);
        speed.z = Mathf.Clamp(speed.z, -maxspeed, maxspeed);

        if (rb.velocity.x > maxspeed || rb.velocity.z > maxspeed || rb.velocity.z < -maxspeed || rb.velocity.x < -maxspeed)
        {
            rb.velocity = new Vector3(speed.x, rb.velocity.y, speed.z);
        }





        


        







    }
    void FixedUpdate()
    {
        
        
        //Adding Force to GameObject
        rb.AddRelativeForce(movement.x, 0, movement.z, ForceMode.VelocityChange);
        if(movement.x != 0 || movement.z != 0)
        {//moving
            EntitySystem.Instance.MainPlayer.Animator.SetBool("Walking", true);
        }
        else
        {
            EntitySystem.Instance.MainPlayer.Animator.SetBool("Walking", false);
        }
        
        
        
       

 
    }
}
