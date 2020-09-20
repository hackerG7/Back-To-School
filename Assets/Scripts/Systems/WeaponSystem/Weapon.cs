using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    public Entity Master;
    public string WeaponID;
    public string AttackSkillID;//The basic attack skill
    public EntitySkill AttackSkill;

    private void Start()
    {
        AttackSkill = EntitySkill.FromSkill(SkillDatabase.Instance.FindSkillByID(AttackSkillID));
    }
    public void Attack()
    {
        AttackSkill.Run(Master);
    }
    private void Update()
    {
        AttackSkill.Update();
    }

}
