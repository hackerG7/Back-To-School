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

    //Camera control   
    public Transform campos;    
    public Vector3 offset;

    //player rotation
    public float sensitivity = 100f;


    void Start()
    {
        
    }



    
    void Update()
    {
        //Control
        movement.x = Input.GetAxisRaw("Horizontal") * movementspeed * Time.deltaTime;
        movement.z = Input.GetAxisRaw("Vertical") * movementspeed * Time.deltaTime;
        //Debug.Log(raylength);


        

        
        


        //camera control
        campos.position = rb.position + offset;


        //player rotation
        float mousex = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        playertransform.Rotate(Vector3.up * mousex);







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
