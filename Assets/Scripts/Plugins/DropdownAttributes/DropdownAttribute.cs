
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DropdownAttribute : PropertyAttribute
{
    public Type Type = null;
    public string ListPath = "";
    public List<Object> List = new List<Object>();
    public int SelectedID = -1;
    public DropdownAttribute(string listPath)
    {
        ListPath = listPath;
    }
}
