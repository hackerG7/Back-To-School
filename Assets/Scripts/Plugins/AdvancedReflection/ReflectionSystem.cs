using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionSystem
{
    public static object GetValue(object master, string path)
    {//ReflectionSystem.GetValue("SkillDatabase.Instance.SkillList[0].skillID");
        return GetValue(master, path.Split('.'));
    }
    public static object GetValue(object master, params string[] args)
    {//ReflectionSystem.GetValue("SkillDatabase", "Instance", "SkillList[0]", "skillID");

        string display = "Reflection trying to get value: ";
        foreach(string str in args)
        {
            display += str+",";
        }
        Debug.Log(display);
        //Trying to know if the SkillDatabase is instance var or static class
        object currentMaster = master;//static class don't have master
        Type currentMasterType = master.GetType();

        ReflectionItem reflectionItem = new ReflectionItem(currentMaster, args[0]);
        if (reflectionItem.ReflectionItemType == ReflectionItemType.CLASS)
        {//static class
            currentMaster = null;//Don't have master for static class
            currentMasterType = reflectionItem.GetStaticClass();
        }
        else
        {//instance variable
            currentMaster = reflectionItem.GetValue();
        }

        for (int i = 1; i < args.Length; i++)
        {
            string target = args[i];
            if (currentMaster == null)
            {//static class
                reflectionItem = new ReflectionItem(currentMasterType, target);
            }
            else
            {//instance variable
                reflectionItem = new ReflectionItem(currentMaster, target);
            }

            if (reflectionItem.ReflectionItemType == ReflectionItemType.CLASS)
            {//static class
                currentMaster = null;//Don't have master for static class
                currentMasterType = reflectionItem.GetStaticClass();
            }
            else
            {//instance variable
                currentMaster = reflectionItem.GetValue();
            }

        }


        //if (currentMaster == null) return (object)currentMasterType;
        return currentMaster;
    }
}
