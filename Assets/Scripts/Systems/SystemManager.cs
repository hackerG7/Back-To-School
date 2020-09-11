using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance;
    private void Awake()
    {
        instance = this;
    }


    public SceneSystem SceneSystem;
}
