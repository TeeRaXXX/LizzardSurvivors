using System.Collections.Generic;
using UnityEngine;

public class GameModeAncients : IGameMode
{
    private BootstrapGameplay _gameplay;
    public void Initialize(List<CharacterType> playersCharacters, BootstrapGameplay gameplay)
    {
        _gameplay = gameplay;
    }
}