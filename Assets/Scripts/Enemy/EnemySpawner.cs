using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private float _rate = 1; // enemies per second
    private float _waveDuration, _spawnT, _waveT;
    private int _enemiesLeft = 0, _currentWave = 0, _enemyCountSpread;

    private void Start()
    {
        _waveDuration = GameManager.GameConfig.waveDuration;
        _enemyCountSpread = GameManager.GameConfig.waveEnemyCountSpread;
        StartWave();
    }

    private void Update()
    {
        _waveT += Time.deltaTime;
        if (_waveT >= _waveDuration)
        {
            StartWave();
            _waveT = 0f;
        }
        
        if (_enemiesLeft <= 0) return;
        _spawnT += Time.deltaTime;
        
        if (_spawnT >= _rate)
        {
            _spawnT = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Debug.Log("Enemies left: " + _enemiesLeft);
        if (_enemiesLeft <= 0) return;
        EnemyPool.GetNewEnemy();
        _enemiesLeft--;
    }

    public void StartWave()
    {
        Debug.Log("Wave start");
        var waveNum = ++_currentWave;
        _enemiesLeft = waveNum + Random.Range(1, _enemyCountSpread);
        _rate = _waveDuration / 3 / _enemiesLeft; // adjust rate to spawn all enemies in first third of wave duration
        _spawnT = _rate; // to spawn first enemy instantly
    }
}