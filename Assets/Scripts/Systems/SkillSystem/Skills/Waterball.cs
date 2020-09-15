using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterball : Skill
{
    public Waterball(string skillID, string skillName, float cooldown)
    : base(skillID, skillName, cooldown)
    {
        // DerivedClass parameter types have to match base class types
        // Do additional work here otherwise you can leave it empty
    }

    public override void Run(Entity entity)
    {//skill content
        entity.Rigidbody.AddForce(0, 10, 0,ForceMode.Impulse);
        Debug.Log($"running skill waterball! flying to sky~");
    }
}
