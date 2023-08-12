public enum GameModes
{
    Survival,
    Ancients,
    Battleroyale
}

public class GameModeBuilder
{
    public IGameMode GetGameMode(GameModes gameMode)
    {
        switch (gameMode)
        { 
            case GameModes.Survival:
                return new GameModeSurvival();
            case GameModes.Ancients:
                return new GameModeAncients();
            case GameModes.Battleroyale:
                return new GameModeBattleroyale();
            default:
                return new GameModeSurvival();
        }
    }
}