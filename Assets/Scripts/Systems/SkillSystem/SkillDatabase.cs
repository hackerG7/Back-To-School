using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SkillDatabase : SingletonMonoBehaviour<SkillDatabase>
{

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
        Debug.LogError("AWAKE");
        LoadAllSkills();//Loading all skills
    }

    public Skill FindSkillByID(string skillID)
    {
        return SkillList.FirstOrDefault(s => s.skillID == skillID);
    }
}
