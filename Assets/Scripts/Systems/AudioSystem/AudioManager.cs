using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sound
{
    public string name;
    public AudioClip clip;


}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; //singleton
    
    void Awake()
    {
        Instance = this;
    }

    public void Play(string name)
    {
        
    }


}
