using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntitySkill
{
    public Skill Skill;
    public bool SkillReady = true;

    public float CurrentTime = 0;
#region Constructor
    public EntitySkill(Skill skill)
    {//Constructor
        Skill = skill;
    }
    public static EntitySkill FromSkill(Skill skill)
    {//static constructor
        return new EntitySkill(skill);
    }
#endregion
    public void Run(Entity entity)
    {//Run the skill~
        if (SkillReady)//if the cooldown loaded completely
        {
            Skill.Run(entity);//Run the skill
            SkillReady = false;//Set the skill ready to false to prevent spamming skills.
            CurrentTime = Skill.Cooldown;//Set the current time to cooldown, then start counting in void Update
        }
        
    }

    //The Entity Monobehaviour would run the update.
    public void Update()
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
