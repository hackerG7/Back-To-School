using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEntity : MonoBehaviour
{
    public Entity Entity;
    public Entity TargetEntity;
    public float MinDistance = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = TargetEntity.transform.position;
        transform.LookAt(targetPosition);

        if (Vector3.Distance(transform.position, targetPosition) >= MinDistance)
        {

            transform.position += transform.forward * Entity.Speed * Time.deltaTime / 10;

        }
    }
}
