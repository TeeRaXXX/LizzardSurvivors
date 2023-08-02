using UnityEngine;

public enum GameModes
{
    Survival,
    Ancients
}

public class GameModeBuilder : MonoBehaviour
{
    public IGameMode GetGameMode(GameModes gameMode)
    {
        switch (gameMode)
        { 
            case GameModes.Survival:
                return new GameModeSurvival();
            case GameModes.Ancients:
                return new GameModeAncients();
            default:
                return new GameModeSurvival();
        }
    }
}