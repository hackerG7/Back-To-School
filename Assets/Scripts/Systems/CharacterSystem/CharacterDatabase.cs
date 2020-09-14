using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterDatabase : MonoBehaviour
{
    public List<Character> CharacterList = new List<Character>();

    public Character FindCharacterByID(string characterID)
    {
        return CharacterList.FirstOrDefault(c => c.CharacterID == characterID);
    }
}
