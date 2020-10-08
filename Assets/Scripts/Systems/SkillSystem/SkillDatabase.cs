using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    public static SkillDatabase Instance;//Singleton
    public List<Skill> SkillList = new List<Skill>();
    

    [Button]
    public void LoadAllSkills()
    {//This is the place holding all skills data
        SkillList = Resources.FindObjectsOfTypeAll<Skill>().ToList();
        EditorUtility.SetDirty(this);
    }
    public void Initiate()
    {
        LoadAllSkills();
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
