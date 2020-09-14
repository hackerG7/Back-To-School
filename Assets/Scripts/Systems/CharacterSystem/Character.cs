using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Character
{
    public string CharacterID = "";
    public string CharacterName = "";

    #region BaseValue
    public float BaseHealth = 100f;
    public float BaseSpeed = 100f;
    public float BaseDamage = 100f;
    #endregion


    #region Skills
    public string SkillID = "";
    public string UltimateSkillID = "";
    #endregion

}
