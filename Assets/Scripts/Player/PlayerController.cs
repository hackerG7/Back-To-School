using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string SkillButton = "Fire2";
    public string UltimateSkillButton = "Jump";
    private void Update()
    {
        if (Input.GetButtonDown(SkillButton))
        {//Run normal skill
            EntitySystem.Instance.MainPlayer.RunSkill();
        }
        if (Input.GetButton(UltimateSkillButton))
        {//Run normal skill
            EntitySystem.Instance.MainPlayer.RunUltimateSkill();
        }
    }

}
