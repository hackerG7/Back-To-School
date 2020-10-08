using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/GrassballSkill")]
public class GrassballSkill : Skill
{
    public override void Run(Entity entity)
    {
        Debug.Log($"running skill grassball!");
    }
}
