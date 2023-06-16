using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnHandler : MonoBehaviour, IInitializeable
{
    [SerializeField] private SOLevel _level;
    [SerializeField] private SOEnemies _allEnemies;
    [SerializeField] private List<BoxCollider2D> _spawnAreas;
    [SerializeField] private int _maxEnemiesCount;

    private int _currentSpawnCollider;
    private EnemiesPerMinute _enemiesToSpawn;
    private int _currentEnemiesCount;

    public void Initialize()
    {
        _enemiesToSpawn = _level.EnemiesWaves[0];
        _currentSpawnCollider = 0;
        _currentEnemiesCount = 0;

        EventManager.OnNewGameMinute.AddListener(UpdateEnemiesList);
        EventManager.OnNewGameSecond.AddListener(SpawnEnemy);
        EventManager.OnEnemyDied.AddListener(OnEnemyDied);
    }

    private void UpdateEnemiesList(int gameMinute)
    {
        if (gameMinute < _level.EnemiesWaves.Count)
            _enemiesToSpawn = _level.EnemiesWaves[gameMinute];

        Debug.Log($"New game minute - {gameMinute}");
    }

    private void SpawnEnemy(int gameTimeInSeconds)
    {
        if (gameTimeInSeconds % _enemiesToSpawn.SpawnFrequency == 0 && _currentEnemiesCount < _maxEnemiesCount)
        {
            GameObject enemyToSpawn = _allEnemies.EnemiesList.Find(obj => obj.EnemyType == GetRandomEnemy()).EnemyPrefab;
            Instantiate(enemyToSpawn, GetSpawnPosition(), new Quaternion());
            _currentEnemiesCount++;
        }
    }

    private EnemyType GetRandomEnemy()
    {
        int biggestPercentIndex = 0;
        float biggestPercent = 0f;

        for (int i = 0; i < _enemiesToSpawn.EnemiesPercents.Count; i++)
        {
            if (Random.Range(0f, 1f) <= _enemiesToSpawn.EnemiesPercents[i].Percent)
            {
                return _enemiesToSpawn.EnemiesPercents[i].Enemy;
            }
            if (_enemiesToSpawn.EnemiesPercents[i].Percent > biggestPercent)
            {
                biggestPercent = _enemiesToSpawn.EnemiesPercents[i].Percent;
                biggestPercentIndex = i;
            }
        }

        return _enemiesToSpawn.EnemiesPercents[biggestPercentIndex].Enemy;
    }

    private Vector3 GetSpawnPosition()
    {
        var colider = _spawnAreas[_currentSpawnCollider];

        if (_currentSpawnCollider == _spawnAreas.Count - 1)
            _currentSpawnCollider = 0;
        else _currentSpawnCollider++;

        return colider.bounds.center + new Vector3(
           (Random.value - 0.5f) * colider.bounds.size.x, (Random.value - 0.5f) * colider.bounds.size.y, 0f);
    }

    private void OnEnemyDied(EnemyType type)
    {
        if (_currentEnemiesCount > 0)
            _currentEnemiesCount--;
    }
}