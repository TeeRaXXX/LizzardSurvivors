using System.Collections.Generic;
using UnityEngine;

public interface IGameMode
{
    public void Initialize(List<CharacterType> playersCharacters, BootstrapGameplay gameplay);   
}