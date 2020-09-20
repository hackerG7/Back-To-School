using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EntityState
{
    public float CurrentTimer = Mathf.Infinity;
    //Parameters
    private Entity Master;//The one who used the skill
    private Entity Target;//The target who are having the state
    public State State;
    public float Duration;
    
    public EntityState(Entity master, Entity target, State state, float duration)
    {
        Master = master;
        Target = target;
        State = state;
        Duration = duration;
        CurrentTimer = Duration;
        
    }
    public void Update()
    {//the entity would call this function every frame

        State.Update(Master, Target);

        if (CurrentTimer > 0)
        {//counting the time
            CurrentTimer-=Time.deltaTime;
        }
        else
        {//Destroy this
            Remove();
        }
    }
    public void Remove()
    {
        Target.StateList.Remove(this);
    }
}
