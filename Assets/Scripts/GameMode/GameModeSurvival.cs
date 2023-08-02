using NastyDoll.Utils;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSurvival : IGameMode
{
    private BootstrapGameplay _gameplay;

    private List<PlayerCharacter> _players;
    private EnemiesSpawnHandler _enemiesSpawnHandler;
    private GameTimer _gameTimer;
    private GameplayUIManager _gameplayUIManager;
    private SkillsSpawner _skillsSpawner;
    private SOCharacters _charactersList;

    public void Initialize(List<CharacterType> playersCharacters, BootstrapGameplay gameplay)
    {
        _gameplay = gameplay;

        _charactersList = gameplay.GetCharactersList;
        _gameTimer = gameplay.SpawnGameplayObject(gameplay.GetGameTimerPrefab, Vector3.zero).GetComponent<GameTimer>();
        _skillsSpawner = gameplay.SpawnGameplayObject(gameplay.GetSkillsSpawnerPrefab, Vector3.zero).GetComponent<SkillsSpawner>();
        _gameplayUIManager = gameplay.SpawnGameplayObject(gameplay.GetGameplayUIManagerPrefab, Vector3.zero).GetComponent<GameplayUIManager>();

        List<Vector3> startPositions = UtilsClass.GetRadialPoints(playersCharacters.Count, 1f);
        _players = new List<PlayerCharacter>();

        for (int i = 0; i < playersCharacters.Count; i++)
        {
            var playerPrefab = gameplay.SpawnGameplayObject(gameplay.GetPlayerCharacterPrefab, startPositions[i]);
            var player = playerPrefab.GetComponent<PlayerCharacter>();
            _players.Add(player);
            player.Initialize(_skillsSpawner,
                              _charactersList.CharactersList.Find(obj => obj.CharacterType == playersCharacters[i]));
        }

        _gameplayUIManager.Initialize(_skillsSpawner);
        _enemiesSpawnHandler = gameplay.SpawnGameplayObject(gameplay.GetEnemiesSpawnHandlerPrefab, Vector3.zero).GetComponent<EnemiesSpawnHandler>();

#if UNITY_EDITOR
        EditorGodMode.Initialize(_skillsSpawner);
#endif

        _gameTimer.Initialize();
        PlayerLevel.Instance.Initialize(_gameplay.StartLevel);
        _enemiesSpawnHandler.Initialize();
    }
}