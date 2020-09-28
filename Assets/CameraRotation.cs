using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public Transform target, player;
    public float rotationspeed = 1f;
    public float mouseX, mouseY;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        CamControl();
    }

    // Update is called once per frame
    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationspeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationspeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);

    }
}
