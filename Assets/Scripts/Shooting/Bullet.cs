using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float AutoRemoveAfterSeconds = 1;

    public void Initiate()
    {
        Invoke("Remove", AutoRemoveAfterSeconds);
    }
    public void Remove()
    {
        MasterObjectPooler.Instance.Release(gameObject, "BulletPool");
    }

}
