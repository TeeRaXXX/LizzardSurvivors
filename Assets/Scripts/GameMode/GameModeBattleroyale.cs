using NastyDoll.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModeBattleroyale : IGameMode
{
    private BootstrapGameplay _gameplay;

    private List<PlayerCharacter> _players;
    private EnemiesSpawnHandler _enemiesSpawnHandler;
    private GameTimer _gameTimer;
    private SkillsSpawner _skillsSpawner;
    private SOCharacters _charactersList;
    private FollowObject _destroyVolume;
    private GameplayUI _gameplayUIManager;

    public void Initialize(List<CharacterType> playersCharacters, BootstrapGameplay gameplay)
    {
        _gameplay = gameplay;

        _charactersList = gameplay.GetCharactersList;
        _gameTimer = gameplay.SpawnGameplayObject(gameplay.GetGameTimerPrefab, Vector3.zero).GetComponent<GameTimer>();
        _skillsSpawner = gameplay.SpawnGameplayObject(gameplay.GetSkillsSpawnerPrefab, Vector3.zero).GetComponent<SkillsSpawner>();
        _skillsSpawner.Initialize(playersCharacters.Count);
        _gameplayUIManager = gameplay.SpawnGameplayObject(gameplay.GetBattleRoyaleUIPrefab, Vector3.zero).GetComponent<GameplayUI>();
        _gameplayUIManager.Initialize(_skillsSpawner, playersCharacters.Count, GameModes.Battleroyale);
        _destroyVolume = gameplay.SpawnGameplayObject(gameplay.GetDestroyVolumePrefab, Vector3.zero).GetComponent<FollowObject>();

#if UNITY_EDITOR
        EditorGodMode.Initialize(_skillsSpawner);
#endif

        List<Vector3> startPositions = UtilsClass.GetRadialPoints(playersCharacters.Count, 7.5f);
        _players = new List<PlayerCharacter>();
        PlayerCharacter.PlayersCount = playersCharacters.Count;

        for (int i = 0; i < playersCharacters.Count; i++)
        {
            var playerPrefab = gameplay.SpawnGameplayObject(gameplay.GetPlayerCharacterPrefab(playersCharacters[i]), startPositions[i]);
            var player = playerPrefab.GetComponent<PlayerCharacter>();
            _players.Add(player);
            player.Initialize(i,
                              _skillsSpawner,
                              _charactersList.CharactersList.Find(obj => obj.CharacterType == playersCharacters[i]),
                              GameModes.Battleroyale);
        }

        _destroyVolume.SetFollowObject(_players[0].gameObject);
        _enemiesSpawnHandler = gameplay.SpawnGameplayObject(gameplay.GetEnemiesSpawnHandlerPrefab, Vector3.zero).GetComponent<EnemiesSpawnHandler>();

        _gameTimer.Initialize();
        PlayerLevel.Instance.Initialize(_gameplay.StartLevel);
        _enemiesSpawnHandler.Initialize();
        gameplay.SpawnGameplayObject(gameplay.GetSoundManagerPrefab, Vector3.zero);

        EventManager.OnPlayerDied.AddListener(OnCharacterDied);

    }

    private void OnCharacterDied(int characterIndex)
    {
        _players.Remove(_players.FirstOrDefault(p => p.PlayerIndex == characterIndex));

        if (_players.Count == 1)
            EventManager.OnGameOverEvent();
    }
}