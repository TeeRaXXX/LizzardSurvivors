using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnHandler : MonoBehaviour
{
    [SerializeField] private SOLevel _level;
    [SerializeField] private SOEnemies _allEnemies;
    [SerializeField] private List<BoxCollider2D> _spawnAreas;
    [SerializeField] private int _maxEnemiesCount;

    private int _currentSpawnCollider;
    private EnemiesPerMinute _enemiesToSpawn;
    private int _currentEnemiesCount;

    public static EnemiesSpawnHandler Instance;

    public void Initialize()
    {
        Instance = this;

        _enemiesToSpawn = _level.EnemiesWaves[0];
        _currentSpawnCollider = 0;
        _currentEnemiesCount = 0;

        EventManager.OnNewGameMinute.AddListener(UpdateEnemiesList);
        EventManager.OnNewGameSecond.AddListener(SpawnEnemy);
        EventManager.OnEnemyDied.AddListener(OnEnemyDied);
    }

    private EnemiesSpawnHandler() { }

    private void UpdateEnemiesList(int gameMinute)
    {
        if (gameMinute < _level.EnemiesWaves.Count)
            _enemiesToSpawn = _level.EnemiesWaves[gameMinute];
        else _enemiesToSpawn = _level.EnemiesWaves[_level.EnemiesWaves.Count - 1];

        SpawnBoss(_enemiesToSpawn.Boss);
    }

    private void SpawnEnemy(int gameTimeInSeconds)
    {
        if (gameTimeInSeconds % _enemiesToSpawn.SpawnFrequency == 0 && _currentEnemiesCount < _maxEnemiesCount)
        {
            int spawnCount = 1;
            var enemy = GetRandomEnemy(out spawnCount);

            GameObject enemyToSpawn = _allEnemies.EnemiesList.Find(obj => obj.EnemyType == enemy).EnemyPrefab;
            
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(enemyToSpawn, GetSpawnPosition(), new Quaternion());
                _currentEnemiesCount++;
                if (_currentEnemiesCount >= _maxEnemiesCount) break;
            }
        }
    }

    private void SpawnBoss(EnemyType boss)
    {
        GameObject bossToSpawn = _allEnemies.EnemiesList.Find(obj => obj.EnemyType == boss).EnemyPrefab;
        Instantiate(bossToSpawn, GetSpawnPosition(), new Quaternion());
        _currentEnemiesCount++;
    }

    private EnemyType GetRandomEnemy(out int spawnCount)
    {
        int biggestPercentIndex = 0;
        float biggestPercent = 0f;

        for (int i = 0; i < _enemiesToSpawn.EnemiesPercents.Count; i++)
        {
            if (Random.Range(0f, 1f) <= _enemiesToSpawn.EnemiesPercents[i].Percent)
            {
                spawnCount = _enemiesToSpawn.EnemiesPercents[i].SpawnCount;
                return _enemiesToSpawn.EnemiesPercents[i].Enemy;
            }
            else if(_enemiesToSpawn.EnemiesPercents[i].Percent > biggestPercent)
            {
                biggestPercent = _enemiesToSpawn.EnemiesPercents[i].Percent;
                biggestPercentIndex = i;
            }
        }

        spawnCount = _enemiesToSpawn.EnemiesPercents[biggestPercentIndex].SpawnCount;
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

    public void SpawnEnemy(EnemyType enemyType, Vector3 spawnPosition)
    {
        GameObject enemyToSpawn = _allEnemies.EnemiesList.Find(obj => obj.EnemyType == enemyType).EnemyPrefab;
        Instantiate(enemyToSpawn, spawnPosition, new Quaternion());
        _currentEnemiesCount++;
    }

    public void SpawnEnemy(EnemyType enemyType, Vector3 spawnPosition, GameObject objectToDelete)
    {
        GameObject enemyToSpawn = _allEnemies.EnemiesList.Find(obj => obj.EnemyType == enemyType).EnemyPrefab;
        Instantiate(enemyToSpawn, spawnPosition, new Quaternion());
        Destroy(objectToDelete);
    }
}