using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Test : MonoBehaviour
{
    public SkillDatabase skillDB;
    public Type type;
    public string testAdvancedSplitString;
    public string testDetectString;
    public List<List<string>> testList = new List<List<string>>(){new List<string>(){"a", "b", "c"}};
    [Button]
    public void AdvancedSplitTest()
    {
        string[] arr = SplitIgnoringSplitterInBrackets(testAdvancedSplitString, ',');
        foreach(string str in arr)
        {
            Debug.Log($"arg: {str}");
        }
    }
    public string[] SplitIgnoringSplitterInBrackets(string str, char splitter)
    {//split ignoring splitter in brackets
        List<int> splitIndexList = new List<int>();//the list of split points
        for(int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if (c == splitter)
            {//check if it is not in a bracket
                if(!IndexIsInBrackets(str, i))
                {
                    splitIndexList.Add(i);
                }
            }
        }

        return SplitAt(str, splitIndexList.ToArray());
    }
    public static string[] SplitAt(string source, params int[] index)
    {
        index = index.Distinct().OrderBy(x => x).ToArray();
        string[] output = new string[index.Length + 1];
        int pos = -1;

        for (int i = 0; i < index.Length; pos = index[i++])
        {
            output[i] = source.Substring(pos+1, index[i] - pos-1);
        }

        output[index.Length] = source.Substring(pos+1);
        return output;
    }
    public bool IndexIsInBrackets(string str, int index)
    {
        bool bracket1Openned = false;//  (
        bool bracket2Openned = false;//  [
        bool bracket3Openned = false;//  {
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            switch (c)
            {
                case '(':
                    bracket1Openned = true;
                    break;
                case ')':
                    bracket1Openned = false;
                    break;
                case '[':
                    bracket2Openned = true;
                    break;
                case ']':
                    bracket2Openned = false;
                    break;
                case '{':
                    bracket3Openned = true;
                    break;
                case '}':
                    bracket3Openned = false;
                    break;
            }
            if (i == index)
            {//check if it is in a bracket
                return bracket1Openned || bracket2Openned || bracket3Openned;
            }
        }
        return false;
    }
    [Button]
    public void Run()
    {
        object result = ReflectionSystem.GetValue(this, "testList[0][1]");
        if (result.GetType() == typeof(List<string>))
        {
            List<string> result1 = (List<string>)result;
            foreach (string str in result1)
            {
                Debug.Log($"result1 item: {str}");
            }
        }
        else
        {
            string result2 = (string)ReflectionSystem.GetValue(this, "testList[0][1]");
            Debug.Log($"result2: {result2}");
        }

        Debug.Log($"skillID of the first skill: {result}");
    }
    [Button]
    public void TestMethodReflection()
    {
        object result = ReflectionSystem.GetValue(this, "SkillDatabase.Instance.FindSkillByID(\"GrassballSkill\").Cooldown");
        Debug.Log($"result: {result.ToString()}");
    }

    [Button]
    public void TestDetectType()
    {
        DetectPrimaryTypeFromString(testDetectString);
    }

    [Button]
    public void TestParse()
    {

        float.Parse("0.75f");
    }
    public Type DetectPrimaryTypeFromString(string str)
    {
        if (str.Length > 0) if (str[0] == '"')
            {//it is a string
                return typeof(string);
            }
        if (str.Length > 0) if (str[0] == '\'')
            {//it is a string
                return typeof(char);
            }
        if (int.TryParse(str, out int i))
        {
            return typeof(int);
        }
        if(double.TryParse(str, out double d))
        {
            return typeof(double);
        }
        return null;
    }
}
