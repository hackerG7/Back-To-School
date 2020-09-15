using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterDatabase : MonoBehaviour
{
    public static CharacterDatabase Instance;//Singleton
    public List<Character> CharacterList = new List<Character>();

    private void Awake()
    {
        Instance = this;//Singleton
    }
    public Character FindCharacterByID(string characterID)
    {//Find a character by ID
        return CharacterList.FirstOrDefault(c => c.CharacterID == characterID);
    }
}
