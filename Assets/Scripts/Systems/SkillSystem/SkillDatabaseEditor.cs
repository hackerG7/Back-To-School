using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillDatabase))]
public class SkillDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SkillDatabase database = (SkillDatabase)target;
        foreach(Skill skill in database.SkillList)
        {
            // Get all public instance properties of skill
            EditorGUILayout.LabelField(skill.skillID);
            Debug.Log("type: " + skill.GetType().Name);
            Debug.Log("properties count: " + skill.GetType().GetProperties().Length);
            foreach (PropertyInfo info in skill.GetType().GetProperties())
            {
                Debug.Log($"info name: {info.Name}");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(info.Name+" : ");
                EditorGUILayout.TextField(info.GetValue(skill).ToString());
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
