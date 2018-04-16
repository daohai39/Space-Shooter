using System.Collections.Generic;
using UnityEngine;

public class BoltManager
{
    private List<Bolt> _playerBolts;
    private List<Bolt> _enemyBolts;
    private void Start()
    {
        _playerBolts = new List<Bolt>();
        _enemyBolts = new List<Bolt>();
    }

    public void AddPlayerBolt(Bolt bolt)
    {
        _playerBolts.Add(bolt);
        Debug.Log("Player shot added");
    }

    public void RemovePLayerBold(Bolt bolt)
    {
        _playerBolts.Remove(bolt);
        Debug.Log("Player shot removed");
    }

    public void AddEnemyBolt(Bolt bolt)
    {
        _enemyBolts.Add(bolt);
        Debug.Log("Enemy shot added");
    }

    public void RemoveEnemyBolt(Bolt bolt)
    {
        _enemyBolts.Remove(bolt);
        Debug.Log("Enemy shot removed");
    }

    public void ClearBolt()
    {
        _playerBolts.Clear();
        _enemyBolts.Clear();
    }

}
