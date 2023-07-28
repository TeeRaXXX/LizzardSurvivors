using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOCharactersList", menuName = "NastyDoll/New Characters List")]
[Serializable] public class SOCharacters : ScriptableObject
{
    public List<SOCharacter> CharactersList;
}