using System.Collections.Generic;
using UnityEngine;

public static class EnemyPool
{
    private static List<Enemy> _enemies = new List<Enemy>();

    public static Enemy GetNewEnemy()
    {
        foreach (var enemy in _enemies)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                enemy.Init();
                return enemy;
            } 
        }
        
        var newEnemyObject = GameObject.Instantiate(Enemy.Prefab);
        var newEnemy = newEnemyObject.GetComponent<Enemy>();
        _enemies.Add(newEnemy);
        return newEnemy;
    }
}