using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class CharacterDatabase : SingletonMonoBehaviour<CharacterDatabase>
{ 
    public List<Character> CharacterList = new List<Character>();


    public Character FindCharacterByID(string characterID)
    {//Find a character by ID
        return CharacterList.FirstOrDefault(c => c.CharacterID == characterID);
    }
}
