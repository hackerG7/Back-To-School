using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string BasicAttackButton = "Fire1";
    public string SkillButton = "Fire2";
    public string UltimateSkillButton = "Jump";
    private void Update()
    {
        if (Input.GetButtonDown(BasicAttackButton))
        {//Run normal skill

            Weapon weapon = EntitySystem.Instance.MainPlayer.Weapon;
            if (weapon != null)
                weapon.Attack();

        }
        if (Input.GetButtonDown(SkillButton))
        {//Run normal skill
            EntitySystem.Instance.MainPlayer.RunSkill();
        }
        if (Input.GetButtonDown(UltimateSkillButton))
        {//Run normal skill
            EntitySystem.Instance.MainPlayer.RunUltimateSkill();
        }
    }

}
