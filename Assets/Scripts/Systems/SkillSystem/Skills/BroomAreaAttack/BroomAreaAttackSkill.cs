using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomAreaAttackSkill : Skill
{

    public override void Run(Entity entity)
    {//skill content
        entity.AddState("BroomAreaAttack", 3);
    }
}
