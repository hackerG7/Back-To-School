using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomPropertyDrawer(typeof(DropdownAttribute))]
public class DropdownAttributeDrawer : PropertyDrawer
{
    public List<Object> SerializedPropertyToList(SerializedProperty property)
    {
        if (!property.isArray) return new List<Object>() { property.objectReferenceValue };
       // Debug.Log($"property is array, size: {property.arraySize}");
        List<Object> result = new List<Object>();
        for (int i = 0; i < property.arraySize; i++)
        {
            SerializedProperty item = property.GetArrayElementAtIndex(i);
            //Debug.Log($"found item in list: {item.name}");
            result.Add(item.objectReferenceValue);
        }
        return result;
    }
    public List<Object> CastObjectToList(object obj)
    {
        IList collection = (IList)obj;
        List<Object> result = new List<Object>();
        foreach(var o in collection)
        {
            result.Add((Object)o);
        }
        Debug.Log($"casting object to list: {obj.GetType()}");
        return result;
    }
    public List<Object> FindList(SerializedObject obj, string path)
    {//Find the list in the serializedobject using the path

        SerializedProperty foundProperty = obj.FindProperty(path);
        //Debug.Log($"Found list result: {result.name}");
        if (foundProperty != null)
        {
            return SerializedPropertyToList(foundProperty);
        }
        else
        {//then try find the list from static class
            //Debug.Log($"Cannot find list from path: {path}, trying static classes");
            string[] items = path.Split('.');
            string typeString = items[0];
            Type staticClass = Type.GetType(typeString);//static class
            Debug.Log($"static class type: {staticClass}");

            #region Try field
            foreach(var field in staticClass.GetFields())
            {
                Debug.Log($"field: {field}");
            }
            FieldInfo fieldInfo = staticClass.GetField(items[1]);//the instance field
            if (fieldInfo != null)
            {
                Debug.Log($"searching info: {items[1]}  ");
                Debug.Log($"found info: {fieldInfo.Name}");
                object result = fieldInfo.GetValue(null);//the instance object
                Debug.Log($"found instance object result: {result.ToString()}");
                for (int i = 2; i < items.Length; i++)
                {
                    Debug.Log($"found result: {result.ToString()}");
                    Debug.Log($"found field: {result.GetType().GetField(items[i])}");
                    result = result.GetType().GetField(items[i]).GetValue(result);//the instance object
                }
                Debug.Log($"found result: {result.ToString()}");
                return CastObjectToList(result);
            }
            #endregion
            #region Try property (with getter and setter)

            foreach (var p in staticClass.GetProperties())
            {
                Debug.Log($"property: {p}");
            }
            PropertyInfo propertyInfo = staticClass.GetProperty(items[1]);//the instance Property
            if (propertyInfo != null)
            {
                Debug.Log($"searching info: {items[1]}  ");
                Debug.Log($"found info: {propertyInfo.Name}");
                object result = propertyInfo.GetValue(null);//the instance object
                Debug.Log($"found instance object result: {result.ToString()}");
                for (int i = 2; i < items.Length; i++)
                {
                    Debug.Log($"found result: {result.ToString()}");
                    Debug.Log($"found Property: {result.GetType().GetProperty(items[i])}");
                    result = result.GetType().GetProperty(items[i]).GetValue(result);//the instance object
                }
                Debug.Log($"found result: {result.ToString()}");
                return CastObjectToList(result);
            }
            #endregion
            return null;
        }
    }
    public string[] ListToStringArray(List<Object> list)
    {
        string[] result = new string[list.Count];
        for(int i = 0; i < list.Count; i++)
        {
            Object obj = (Object)list[i];
            result[i] = obj.name;
        }
        return result;
    }
    public int FindSelectedID(List<Object> list, Object obj)
    {
        return list.IndexOf(obj);
    }
    private IList CreateListWithCustomType(Type itemType)
    {
        var listType = typeof(List<>);
        var constructedListType = listType.MakeGenericType(itemType);
        var instance = Activator.CreateInstance(constructedListType);
        return (IList)instance;
    }
    private List<T> CastWholeList<T>(object list, Type itemType)
    {//CastWholeList(new List<int>(){1, 2, 3}, string)
        IList oldList = (IList)list;
        IList newList = CreateListWithCustomType(itemType);
        foreach(var item in oldList)
        {
            newList.Add(item);
        }
        return (List<T>)newList;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DropdownAttribute dropdownAttribute = (DropdownAttribute)attribute;
        Type itemType = dropdownAttribute.Type;
        object listObj = ReflectionSystem.GetValue(property.serializedObject.targetObject, dropdownAttribute.ListPath);
        Debug.Log(listObj);


        var list = CastWholeList<Object>(listObj, typeof(Object));//FindList(property.serializedObject, dropdownAttribute.ListPath);
        string[] stringArray = ListToStringArray(list);
        Object obj = property.objectReferenceValue;

        #region Draw the list dropdown
        GUIContent dropdown = new GUIContent(property.name);

        int SelectedID = FindSelectedID(list, obj);//Update the selectedID
        int newSelectedID = EditorGUILayout.Popup(dropdown, SelectedID, ListToStringArray(list));
        if(newSelectedID != SelectedID)
        {//changed
            SelectedID = newSelectedID;
            Object selectedObject = (Object)list[SelectedID];
            property.objectReferenceValue = selectedObject;
            //EditorUtility.SetDirty(property.serializedObject.targetObject);//repaint
            Debug.Log($"changed to {property.objectReferenceValue.name}");
        }
        
        #endregion

    }
}
