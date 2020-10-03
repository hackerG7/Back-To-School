using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public Transform target, player;
    public float rotationspeed = 1f;
    public float mouseX, mouseY;
    public Transform record;

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
        if (Physics.Linecast(target.position,record.position,out hit))
        {
            transform.position = hit.point;
        }
        else
        {
            transform.localPosition = record.localPosition;
        }
        Debug.Log(hit.point);
        Debug.DrawLine(target.position, record.position, Color.blue);
    }
}
