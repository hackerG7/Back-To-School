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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update all EntitySkills
        foreach(EntitySkill entitySkill in SkillList)
        {//updating each EntitySkill
            entitySkill.Update();
        }

    }
}
