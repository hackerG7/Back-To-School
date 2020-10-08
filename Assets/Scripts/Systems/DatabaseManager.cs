using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitiateAllDatabases();
    }
    public void InitiateAllDatabases()
    {//Initiate all databases
        SkillDatabase.Instance.Initiate();
        WeaponDatabase.Instance.Initiate();
    }
}
