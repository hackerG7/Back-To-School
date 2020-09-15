﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grassball : Skill
{
    public Grassball(string skillID, string skillName, float cooldown)
    : base(skillID, skillName, cooldown)
    {
        // DerivedClass parameter types have to match base class types
        // Do additional work here otherwise you can leave it empty
    }

    public override void Run(Player player)
    {
        Debug.Log($"running skill grassball!");
    }
}
