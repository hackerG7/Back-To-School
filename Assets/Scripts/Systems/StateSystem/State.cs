using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class State : MonoBehaviour
{
    public string StateID;//Identifier
    public string StateName;//The name of the state

    public State(string stateID, string stateName)
    {
        StateID = stateID;
        StateName = stateName;
    }
    // Update is called at the beginning of the state
    public virtual void RunStart(Entity master, Entity target)
    {

    }
    // Update is called once per frame
    public virtual void RunUpdate(Entity master, Entity target)
    {
        
    }

    // Update is called at the end of the state
    public virtual void RunEnd(Entity master, Entity target)
    {

    }
}
