using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StateDatabase : MonoBehaviour
{

    public static StateDatabase Instance;//Singleton
    public List<State> StateList = new List<State>();

    private void LoadAllStates()
    {//This is the place holding all States data
        StateList.Add(new AreaAttackState("BroomAreaAttack", "掃把旋轉術"));
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
