using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySystem : MonoBehaviour
{
    public static EntitySystem Instance;//Singleton
    public List<Entity> Entities = new List<Entity>();
    public Player MainPlayer;
    public void LoadAllEntities()
    {
        Entities = new List<Entity>(transform.GetComponentsInChildren<Entity>());
        
    }
    private void Awake()
    {
        Instance = this;//Singleton
        LoadAllEntities();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
