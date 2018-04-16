using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    private List<Enemy> _enemies;

    public EnemyManager()
    {
        _enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        Debug.Log("Enemy add to game: " + enemy.name);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.DestroySelf();
        Debug.Log("Enemy remove from game: " + enemy.name);
    }

    public void ClearEnemy()
    {
        foreach(var enemy in _enemies)
        {
            _enemies.Remove(enemy);
            enemy.DestroySelf();
        }
        Debug.Log("Remove all enemy from game");
    }

    public Enemy GetEnemy(int id) {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].Id == id) {
                return _enemies[i];
            }
        }

        return null;
    }

    public void DestroyEnemy(int id) {
        Enemy enemyNeedToDestroy = GetEnemy(id);
        if (enemyNeedToDestroy == null) return;
        _enemies.Remove(enemyNeedToDestroy);
        Debug.Log("Enemy remove from game: " + enemyNeedToDestroy.name); 
        enemyNeedToDestroy.DestroySelf();
    }
}
