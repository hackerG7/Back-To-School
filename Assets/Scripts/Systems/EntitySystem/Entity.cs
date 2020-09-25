using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float Health = 100;
    public float Speed = 100;
    public float Damage = 100;
    public Rigidbody Rigidbody;
    public List<EntitySkill> SkillList = new List<EntitySkill>();
    public List<EntityState> StateList = new List<EntityState>();
    public MasterObjectPooler ObjectPooler;
    public Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Update all EntitySkills
        UpdateEntitySkill();
        //Update all EntityStates
        UpdateEntityState();
        //Death
        if(Health <= 0)
        {
            gameObject.SetActive(false);//die when no health
        }

    }
    public void UpdateEntitySkill()
    {
        foreach (EntitySkill entitySkill in SkillList)
        {//updating each EntitySkill
            entitySkill.Update();
        }
    }
    public void UpdateEntityState()
    {
        for (int i = 0; i < StateList.Count; i++)
        {//updating each EntitySkill
            EntityState entityState = StateList[i];
            entityState.Update();
            if (entityState.GetFinished())
            {
                entityState.End();//End the state
                StateList.Remove(entityState);
                i--;
            }
        }
    }

    #region AddState
    public void AddState(Entity master, State state, float duration)
    {//AddState with given other master, 由其他人施放的效果
        EntityState ES = new EntityState(master, this, state, duration);
        ES.Start();//Start the state
        StateList.Add(ES);
    }
    public void AddState(Entity master, string stateID, float duration)
    {//AddState with given other master, 由其他人施放的效果
        State state = StateDatabase.Instance.FindStateByID(stateID);
        if (state == null) return;
        AddState(master, state, duration);
    }
    public void AddState(State state, float duration)
    {
        AddState(this, state, duration);
    }
    public void AddState(string stateID, float duration)
    {
        State state = StateDatabase.Instance.FindStateByID(stateID);
        if (state == null) return;
        AddState(state, duration);
    }
    #endregion

    #region Animator
    public void RunWeaponAttackAnimation()
    {
        Animator.SetTrigger("WeaponAttack");
        //Animator.SetBool("WeaponAttacking", true);
    }
    #endregion
}
