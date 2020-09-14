using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public Skill Skill;
    public bool SkillReady = true;

    public float CurrentTime = 0;

    public void Run(Player player)
    {//Run the skill~
        if (SkillReady)//if the cooldown loaded completely
        {
            Skill.Run(player);//Run the skill
            SkillReady = false;//Set the skill ready to false to prevent spamming skills.
            CurrentTime = Skill.Cooldown;//Set the current time to cooldown, then start counting in void Update
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(CurrentTime > 0)
        {//Counting the cooldown. 
            CurrentTime -= Time.deltaTime;
        }
        else
        {
            if (!SkillReady)
            {//Now the skill state turned into ready. 
                SkillReady = true;
            }
        }
    }
}
