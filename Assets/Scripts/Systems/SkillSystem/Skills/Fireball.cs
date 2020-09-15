﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Skill
{
    public Fireball(string skillID, string skillName, float cooldown)
    : base(skillID, skillName, cooldown)
    {
        // DerivedClass parameter types have to match base class types
        // Do additional work here otherwise you can leave it empty
    }

    public override void Run(Entity entity)
    {
        BulletSystem.Instance.CreateBullet("Fireball").Shoot(entity, 1000);
        Debug.Log($"running skill fireball!");
    }
}
