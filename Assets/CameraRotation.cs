using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public Transform player;
    public float XAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yrotation = player.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(XAngle, yrotation, transform.rotation.z);

    }
}
