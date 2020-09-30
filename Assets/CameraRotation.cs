using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public Transform target, player;
    public float rotationspeed = 1f;
    public float mouseX, mouseY;

    private RaycastHit hit;
    private Vector3 camera_offset;
    
    // Start is called before the first frame update
    void Start()
    {
        camera_offset = transform.localPosition;
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

    void Update()
    {
        if (Physics.Linecast(target.position,target.position + camera_offset,out hit))
        {
            transform.localPosition = new Vector3(0, 0, -Vector3.Distance(target.position, hit.point));
        }
        else
        {
            transform.localPosition = camera_offset;
        }
        Debug.Log(hit.point);
    }
}
