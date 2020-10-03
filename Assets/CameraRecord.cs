using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecord : MonoBehaviour
{
    public Transform Camera;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
