using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    public static SkillDatabase Instance;//Singleton
    public List<Skill> SkillList = new List<Skill>();

    [Button]
    public void LoadAllSkills()
    {//This is the place holding all skills data
        SkillList = gameObject.GetComponentsInChildren<Skill>().ToList();
        /*
        SkillList.Add(new Fireball("Fireball", "測試火焰術", 1));
        SkillList.Add(new Waterball("Waterball", "測試水球術", 5));
        SkillList.Add(new Grassball("Grassball", "測試綠葉術", 1));
        SkillList.Add(new Fireball("FireballSuper", "烈焰火球術", 5).SetPower(1000));
        SkillList.Add(new BroomAreaAttackSkill("BroomAreaAttack", "掃把旋轉術", 5));
        SkillList.Add(new BroomTornado("BroomTornado", "掃把龍捲風", 3));

        //Weapon attack 
        SkillList.Add(new MeleeAttack("BroomAttack", "掃把攻擊", 0.05f));

        */
    }
    private void Awake()
    {
        Instance = this;//Singleton
        LoadAllSkills();//Loading all skills
    }

    public Skill FindSkillByID(string skillID)
    {
        return SkillList.FirstOrDefault(s => s.skillID == skillID);
    }
}
