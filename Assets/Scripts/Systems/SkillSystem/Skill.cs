using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill : MonoBehaviour
{
    public Skill(string skillID, string skillName, float Cooldown)
    {//Constructor
        this.skillID = skillID;
        this.skillName = skillName;
        this.Cooldown = Cooldown;
    }

    public string skillID = "";
    public string skillName = "";

    public float Cooldown = 5;//set the basic cooldown to 5 seconds

    public virtual void Run(Entity entity)
    {
        Debug.Log($"running skill {skillName}");
    }
}
