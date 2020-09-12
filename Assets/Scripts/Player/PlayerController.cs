using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public Shooting Shooting;


    //Singleton
    public static PlayerController Instance;
    void Awake()
    {
        Instance = this;
    }
    //Singleton


}
