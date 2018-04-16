using System.Collections.Generic;
using UnityEngine;

public class BoltManager
{
    private List<Bolt> _playerBolts;
    private List<Bolt> _enemyBolts;
    private int _emBoltId;
    private int _playerBoltId;
    private void Start()
    {
        _playerBolts = new List<Bolt>();
        _enemyBolts = new List<Bolt>();
        _emBoltId = 0;
        _playerBoltId = 0;
    }

    public Bolt GetPlayerBolt(int id)
    {
        for (var i = 0; i < _playerBolts.Count; i++)
        {
            if (_playerBolts[i].Id == id)
                return _playerBolts[i];
        }

        return null;
    }

    public Bolt GetEnemyBolt(int id)
    {
        for (var i = 0; i < _enemyBolts.Count; i++)
        {
            if (_enemyBolts[i].Id == id)
                return _enemyBolts[i];
        }

        return null;
    }
    
    public void AddPlayerBolt(Bolt bolt)
    {
        bolt.Id = _playerBoltId;
        _playerBolts.Add(bolt);
        _playerBoltId++;
        Debug.Log("Player shot added");
    }

    public void RemovePLayerBolt(Bolt bolt)
    {
        _playerBolts.Remove(bolt);
        Debug.Log("Player shot removed");
    }

    public void AddEnemyBolt(Bolt bolt)
    {
        bolt.id = _enemyBoltId;
        _enemyBolts.Add(bolt);
        _enemyBoltId++;
        Debug.Log("Enemy shot added");
    }

    public void RemoveEnemyBolt(Bolt bolt)
    {
        _enemyBolts.Remove(bolt);
        Debug.Log("Enemy shot removed");
    }

    public void RemovePLayerBolt(int id)
    {
        var playerBolt = GetPlayerBolt(id);
        if (playerBolt == null) return;
        _playerBolts.Remove(playerBolt);
        playerBolt.DestroySelf();
    }

    public void RemoveEnemyBolt(int id)
    {
        var enemyBolt = GetEnemyBolt(id);
        if (enemyBolt == null) return;
        _enemyBolts.Remove(enemyBolt);
        enemyBolt.DestroySelf();
    }

    public void ClearBolt()
    {
        foreach (var bolt in _playerBolts)
        {
            _playerBolts.Remove(bolt);
            bolt.DestroySelf();
        }

        foreach (var bolt in _enemyBolts)
        {
            _enemyBolts.Remove(bolt);
            bolt.DestroySelf();
        }

        _emBoltId = 0;
        _playerBoltId = 0;
    }

}
