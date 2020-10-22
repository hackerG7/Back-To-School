using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    // Variable
    public int money = 0;
    public int diamond = 0;

    
    public static CurrencySystem Instance;
    
    
    private void Awake()
    {
        Instance = this;
    }
  
    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
    }

    public void TakeMoney(int moneyToTake)
    {
        if (money < moneyToTake)
        {
            //no enough money
        } 
        else
        {
            money -= moneyToTake;
        }
    }

    public void SetMoney(int moneyToSet)
    {
        if (moneyToSet < 0)
        {
            //wrong input
        }
        else
        {
            money = moneyToSet;
        }
    }

    public void AddDiamond(int diamondToAdd)
    {
        diamond += diamondToAdd;
    }

    public void TakeDiamond(int diamondToTake)
    {
        if (diamond < diamondToTake)
        {
            //no enough diamond
        }
        else
        {
            diamond -= diamondToTake;
        }
    }

    public void SetDiamond(int diamondToSet)
    {
        if (diamondToSet < 0)
        {
            //wrong input
        }
        else
        {
            diamond = diamondToSet;
        }
    }

    public int GetMoney()
    {
        return money;
    }

    public int GetDiamond()
    {
        return diamond;
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
