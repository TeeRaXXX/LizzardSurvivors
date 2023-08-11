public enum GameModes
{
    Survival,
    Ancients,
    Battleroyal
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
            case GameModes.Battleroyal:
                return new GameModeBattleroyal();
            default:
                return new GameModeSurvival();
        }
    }
}