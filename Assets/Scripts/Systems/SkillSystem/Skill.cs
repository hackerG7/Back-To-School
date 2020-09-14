using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
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

    public virtual void Run(Player player)
    {
        Debug.Log($"running skill {skillName}");
    }
}
