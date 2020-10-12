using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

public class ReflectionItem
{
    public ReflectionItemType ReflectionItemType;
    public object Master;//the master object
    public Type MasterType;//the master object
    public string Target;//the Target variable name

    public ReflectionItem(object master, string target)
    {
        Master = master;
        MasterType = master.GetType();
        Target = target;
        ReflectionItemType = DetectReflectionItemType(target);//Detect the type first
        Debug.Log($"new Reflection Item   Master(instance variable): {MasterType.Name}  Target({ReflectionItemType}): {Target}");
    }
    public ReflectionItem(Type masterType, string target)
    {
        MasterType = masterType;
        Target = target;
        ReflectionItemType = DetectReflectionItemType(target);//Detect the type first
        Debug.Log($"new Reflection Item Master(static class): {MasterType.Name} Target({ReflectionItemType}): {Target}");
    }
    private ReflectionItemType DetectReflectionItemType(string Target)
    {
        if (Target.Contains("."))
        {//ERROR
            Debug.LogError($"ReflectionItemTypeDetector.Detect   Invalid input containing dots: {Target}");
            return ReflectionItemType.NULL;
        }

        if (Target.Contains("(") && Target.Contains(")"))
        {
            return ReflectionItemType.METHOD;
        }
        if (Target.Contains("[") && Target.Contains("]"))
        {
            return ReflectionItemType.COLLECTION;
        }
        //It is instance or class
        object instanceVariable = FindInstanceVariable(Target);
        Type staticClass = FindStaticClass(Target);

        bool couldBeInstance = (instanceVariable != null);
        bool couldBeClass = (staticClass != null);


        if(couldBeInstance && couldBeClass)
        {//WARNING
            Debug.LogError($"Reflection confused. the target {Target} in {MasterType} could be instance or class at the same time!");
            return ReflectionItemType.InstanceOrClass;
        }

        if (couldBeInstance) return ReflectionItemType.INSTANCE;

        if (couldBeClass) return ReflectionItemType.CLASS;

        return ReflectionItemType.NULL;

    }
    public object GetValue()
    {
        switch (ReflectionItemType)
        {
            case ReflectionItemType.INSTANCE:
                return FindInstanceVariable(Target);
            case ReflectionItemType.CLASS:
                Debug.LogError("Please use GetStaticClass() instead of GetValue() if the ReflectionItemType is Class");
                return FindStaticClass(Target);
            case ReflectionItemType.METHOD:
                return FindMethodResult();
            case ReflectionItemType.COLLECTION:
                return FindCollectionItem(Target);
        }
        return null;
    }
    public Type GetStaticClass()
    {
        return FindStaticClass(Target);
    }

    #region Finding Utils
    #region Find Instance Variable
    private object FindInstanceVariable(string Target)
    {
        //Try by field
        object resultByField = FindInstanceVariableByField(Target);
        if (resultByField != null) return resultByField;

        //Try by property
        object resultByProperty = FindInstanceVariableByProperty(Target);
        if (resultByProperty != null) return resultByProperty;

        return null;
    }
    private object FindInstanceVariableByField(string Target)
    {
        Type currentType = MasterType;
        FieldInfo info = currentType?.GetField(Target);
        while(info == null)
        {
            if (currentType == null) return null;
            currentType = currentType.BaseType;
            info = currentType?.GetField(Target);
        }
        Debug.Log($"info {info.Name}");
        return info.GetValue(Master);
        
    }
    private object FindInstanceVariableByProperty(string Target)
    {
        Type currentType = MasterType;
        PropertyInfo info = currentType?.GetProperty(Target);
        while (info == null)
        {
            if (currentType == null) return null;
            currentType = currentType.BaseType;
            info = currentType?.GetProperty(Target);
        }
        return info.GetValue(Master);

    }
    #endregion
    #region Find Static Class
    private Type FindStaticClass(string Target)
    {
        return Type.GetType(Target);
    }
    #endregion
    #region Find Method Result (Fixing)
    private object FindMethodResult()
    {
        MethodInfo info = MasterType?.GetMethod(GetMethodNameFromTarget());
        if (info == null) return null;
        foreach(object para in GetParametersFromTarget())
        {
            Debug.Log($"para  type: {para.GetType()}   value: {para}");
            Debug.Log($"{para.ToString()}");
        }
        Debug.Log($"Invoked method {info.Name} parameters  result: {info.Invoke(Master, GetParametersFromTarget())}");
        return info.Invoke(Master, GetParametersFromTarget());
    }
    public string GetMethodNameFromTarget()
    {
        return Target.Split('(')[0];//"Fuck(`you`, `your mom`)"  ->  "Fuck"
    }
    private object ParseToType(object obj, Type type)
    {
        var converter = TypeDescriptor.GetConverter(type);
        var result = converter.ConvertFrom(obj);
        return result;
    }
    public object[] GetParametersFromTarget()
    {
        string parameterPart = Target.Split(new[] { '(' }, 2)[1];//"GetName(minecraft, getPlayer(world, uuid))"   ->   "minecraft, getPlayer(world, uuid))"
        parameterPart = parameterPart.Substring(0, parameterPart.Length - 1);//"minecraft, getPlayer(world, uuid))"   ->   "minecraft, getPlayer(world, uuid)"
        string[] stringParameterArray = SplitIgnoringSplitterInBrackets(parameterPart, ',');
        object[] result = new object[stringParameterArray.Length];
        for (int i = 0 ; i < stringParameterArray.Length; i++)
        {//turrning string array into object array
            string str = stringParameterArray[i];
            Type primaryType = DetectPrimaryTypeFromString(str);
            if(primaryType == typeof(string))
            {
                result[i] = str.Substring(1, str.Length - 2);//remove the quotes "
                continue;
            }
            else if(primaryType == typeof(char))
            {
                result[i] = str.Substring(1, str.Length - 2).ToCharArray()[0];//remove the quotes '
                continue;
            }
            else if (primaryType != null)
            {//Parse if it is primary type
                result[i] = ParseToType(str, primaryType);//parse it from "3.53f" to 3.53(float)
                continue;
            }
            else
            {//It is not primary type, try object value
                object objValue = ReflectionSystem.GetValue(Master, str);
                if (objValue != null)
                {//it is object
                    result[i] = objValue;
                    continue;
                }
            }
            result[i] = null;


        }
        return result;
    }
    private Type DetectPrimaryTypeFromString(string str)
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
        if (double.TryParse(str, out double d))
        {
            return typeof(double);
        }
        return null;
    }
    #region Method splitting
    public string[] SplitIgnoringSplitterInBrackets(string str, char splitter)
    {//split ignoring splitter in brackets
        List<int> splitIndexList = new List<int>();//the list of split points
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if (c == splitter)
            {//check if it is not in a bracket
                if (!IndexIsInBrackets(str, i))
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
            output[i] = source.Substring(pos + 1, index[i] - pos - 1);
        }

        output[index.Length] = source.Substring(pos + 1);
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

    #endregion
    #endregion
    #region Find Collection Item
    private object FindCollectionItem(string Target)
    {
        string collectionName = GetCollectionNameFromTarget();
        object collectionObject = FindInstanceVariable(collectionName);//SkillList
        int[] collectionIndexes = GetCollectionIndexesFromTarget();//[0,2,5]
        Debug.Log($"indexes length: {collectionIndexes.Length}");
        object currentCollectionObject = collectionObject;
        for(int i = 0; i <collectionIndexes.Length; i++)
        {
            int currentIndex = collectionIndexes[i];
            currentCollectionObject = GetValueInCollection(currentCollectionObject, currentIndex);
        }
        return currentCollectionObject;


    }
    private object GetValueInCollection(object collectionObject, int index)
    {
        Debug.Log($"is array: {collectionObject.GetType().IsArray},  type: {collectionObject.GetType()}");
        IList list = (IList)collectionObject;
        Debug.Log($"Getting value in collection, length: {list.Count}");
        return list[index];
    }
    private string GetCollectionNameFromTarget()
    {
        return Target.Split('[')[0];
    }
    private int[] GetCollectionIndexesFromTarget()
    {
        string parameterPart = Target.Split(new [] {'['}, 2)[1];//"list[0][3][2]"   ->   "0][3][2]"
        parameterPart = parameterPart.Substring(0, parameterPart.Length - 1);//"0][3][2]"   ->   "0][3][2"
        string[] stringArray = parameterPart.Split(new string[] { "][" }, StringSplitOptions.None);//"0][3][2"   ->   ["0","3","2"]
        int[] result = new int[stringArray.Length];
        for(int i = 0; i < result.Length; i++)
        {
            result[i] = int.Parse(stringArray[i]);
        }
        return result;
    }
    #endregion
    #endregion
}
