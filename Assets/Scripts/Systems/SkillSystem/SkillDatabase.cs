using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    public static SkillDatabase Instance;//Singleton
    public List<Skill> SkillList = new List<Skill>();

    private void LoadAllSkills()
    {//This is the place holding all skills data
        SkillList.Add(new Fireball("Fireball", "測試火焰術", 1));
        SkillList.Add(new Waterball("Waterball", "測試水球術", 1));
        SkillList.Add(new Grassball("Grassball", "測試綠葉術", 1));
    }
    private void Awake()
    {
        LoadAllSkills();//Loading all skills
        Instance = this;//Singleton
    }

    public Skill FindSkillByID(string skillID)
    {
        return SkillList.FirstOrDefault(s => s.skillID == skillID);
    }
}
