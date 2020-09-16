using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsExtension
{
    public static void Explosion(Vector3 position, float explosionRadius, float explosionForce)
    {
        Collider[] objects = UnityEngine.Physics.OverlapSphere(position, explosionRadius);
        foreach (Collider h in objects)
        {
            Rigidbody r = h.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.AddExplosionForce(explosionForce, position, explosionRadius);
            }
        }
    }
}
