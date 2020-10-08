using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Create Skill")]
public partial class Skill : ScriptableObject
{
    public string skillID = "";
    public string skillName = "";
    public string description = "<no description>";

    public float Cooldown = 5;//set the basic cooldown to 5 seconds

    public virtual void Run(Entity entity)
    {
        Debug.Log($"running skill {skillName}");
    }
}
