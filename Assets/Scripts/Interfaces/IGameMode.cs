using System.Collections.Generic;

public interface IGameMode
{
    public void Initialize(List<CharacterType> playersCharacters, BootstrapGameplay gameplay);   
}