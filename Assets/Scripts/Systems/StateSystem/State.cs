using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class State
{
    public string StateID;//Identifier
    public string StateName;//The name of the state

    public State(string stateID, string stateName)
    {
        StateID = stateID;
        StateName = stateName;
    }
    // Update is called at the beginning of the state
    public virtual void Start(Entity master, Entity target)
    {

    }
    // Update is called once per frame
    public virtual void Update(Entity master, Entity target)
    {
        
    }

    // Update is called at the end of the state
    public virtual void End(Entity master, Entity target)
    {

    }
}
