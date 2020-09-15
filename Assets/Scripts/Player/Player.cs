﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public string CharacterID;

    //Scripts
    public PlayerController PlayerController;
    public PlayerMovement PlayerMovement;
    public PlayerShooting PlayerShooting;

    //Skills
    public string SkillID = null;
    public string UltimateSkillID = null;
    private void Start()
    {
        SyncStatWithCharacter();//sync the stat value of the player e.g. health, speed, damage
        AddPlayerSkills();//turning the player skillID to the skill and push into the skill list.
    }

    #region Skill
    public EntitySkill GetSkill()
    {//getting the normal skill
        return SkillList[0];
    }
    public EntitySkill GetUltimateSkill()
    {//getting the ultimate skill
        return SkillList[1];
    }
    public void RunSkill()
    {//Run the normal skill
        GetSkill().Run(this);
    }
    public void RunUltimateSkill()
    {//run the ultimate skill
        GetUltimateSkill().Run(this);
    }
    #endregion

    #region Utils

    private Character FindCharacterFromDatabase()
    {
        Character character = CharacterDatabase.Instance.FindCharacterByID(CharacterID);//Find the character from the database

        if (CharacterID == "")
            Debug.LogError($"The main player has no character ID. Please check Hierarchy > EntitySystem > MainPlayer");//show the error

        if (character == null)
            Debug.LogError($"Cannot find character by ID: {CharacterID}");//show the error

        return character;
    }
    private void SyncStatWithCharacter()
    {//Sync the statistic with the given character ID

        Character character = FindCharacterFromDatabase();
        Health = character.BaseHealth;
        Speed = character.BaseSpeed;
        Damage = character.BaseDamage;
    }
    private void AddPlayerSkills()
    {//Add the player skills to the entity skill list

        Skill skill = SkillDatabase.Instance.FindSkillByID(SkillID);//Find the skill from the database
        EntitySkill entitySkill = EntitySkill.FromSkill(skill);//turn the skill into entity skill with cooldown timer
        SkillList.Add(entitySkill);//Adding the normal skill

        Skill ultimateSkill = SkillDatabase.Instance.FindSkillByID(UltimateSkillID);//Find the skill from the database
        EntitySkill entityUltimateSkill = EntitySkill.FromSkill(ultimateSkill);//turn the skill into entity skill with cooldown timer
        SkillList.Add(entityUltimateSkill);//Adding the normal skill
    }
    #endregion
}
