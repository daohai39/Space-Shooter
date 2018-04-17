using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    private List<Enemy> _enemies;
    private int _id;

    public EnemyManager()
    {
        _enemies = new List<Enemy>();
        _id = 0;
    }

    public void AddEnemy(Enemy enemy)
    {
        if (enemy == null) 
            return;
        enemy.Id = _id;
        _id++;
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.DestroySelf();
    }

    public void ClearEnemy()
    {
        foreach(var enemy in _enemies.ToArray())
        {
            _enemies.Remove(enemy);
            enemy.DestroySelf();
        }
        _enemies.Clear();
        _id = 0;
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
        enemyNeedToDestroy.DestroySelf();
    }
}
