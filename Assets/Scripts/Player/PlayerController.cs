using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public PlayerShooting PlayerShooting;


    //Singleton
    public static PlayerController Instance;
    void Awake()
    {
        Instance = this;
    }
    //Singleton


}
