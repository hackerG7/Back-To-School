using QFSW.MOP2;
using ScriptableObjectDropdown;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Variables
    public float Health = 100;
    public float Speed = 100;
    public float Damage = 100;
    public Rigidbody Rigidbody;
    public MasterObjectPooler ObjectPooler;
    public Animator Animator;
    #endregion

    #region Weapon
    [Dropdown("WeaponDatabase.Instance.WeaponList")] public Weapon WeaponReference = null;//This is a field for developer to fill in
    [HideInInspector] public EntityWeapon Weapon;//This is the actual Weapon
    #endregion

    #region Skill and State
    [HideInInspector] public List<EntitySkill> SkillList = new List<EntitySkill>();
    [HideInInspector] public List<EntityState> StateList = new List<EntityState>();
    #endregion

    public void Start()
    {
        SetWeapon(WeaponReference);//Set the weapon for the entity from the reference field
    }
    
    #region Update
    // Update is called once per frame
    void FixedUpdate()
    {
        //Update all EntitySkills
        UpdateEntitySkill();
        //Update all EntityStates
        UpdateEntityState();
        //Update Weapon
        Weapon.Update();

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
    #endregion

    #region SetWeapon
    public void SetWeapon(EntityWeapon weapon)
    {
        Weapon = weapon;
    }
    public void SetWeapon(Weapon weapon)
    {
        Weapon = EntityWeapon.FromWeapon(weapon);
    }
    public void SetWeapon(ScriptableObjectReference weaponReference)
    {
        Weapon weapon = (Weapon)weaponReference.value;//take the value from the input field
        this.Weapon = EntityWeapon.FromWeapon(weapon);//turn it into the EntityWeapon
        Debug.Log($"weapon: {Weapon.Weapon.WeaponName}");
    }
    public void SetWeapon(string weaponID)
    {
        Weapon weapon = WeaponDatabase.Instance.FindWeaponByID(weaponID);
        if (weapon != null)
        {
            EntityWeapon EW = new EntityWeapon(weapon);
            Weapon = EW;
        }
    }
    #endregion
    
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
