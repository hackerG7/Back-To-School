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
        SkillList.Add(new Skill("test1", "測試火焰術", 5));
        SkillList.Add(new Skill("test2", "測試水球術", 5));
        SkillList.Add(new Skill("test3", "測試綠葉術", 5));
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
