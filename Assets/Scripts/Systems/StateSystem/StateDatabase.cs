using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EasyButtons;

public class StateDatabase : MonoBehaviour
{

    public static StateDatabase Instance;//Singleton
    public List<State> StateList = new List<State>();

    [Button]
    public void LoadAllStates()
    {//This is the place holding all skills data
        StateList = gameObject.GetComponentsInChildren<State>().ToList();
    }
    private void Awake()
    {
        LoadAllStates();//Loading all States
        Instance = this;//Singleton
    }

    public State FindStateByID(string StateID)
    {
        return StateList.FirstOrDefault(s => s.StateID == StateID);
    }
}
