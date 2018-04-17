using System.Collections.Generic;
using UnityEngine;


public class BoltManager<T> where T : Bolt
{
    private List<T> _bolts;
    private int _id;

    public BoltManager()
    {
        _bolts = new List<T>();
        _id = 0;
    }

    public T GetBolt(int id)
    {
        for (var i = 0; i < _bolts.Count; i++)
        {
            if (_bolts[i].Id == id)
                return _bolts[i];
        }

        return null;
    }

    public void AddBolt(T bolt)
    {
        if (bolt == null) return;
            
        bolt.Id = _id;
        _bolts.Add(bolt);
        _id++;
    }

    public void RemoveBolt(T bolt)
    {
        _bolts.Remove(bolt);
        bolt.DestroySelf();
    }

    public void RemoveBolt(int id)
    {
        var bolt = GetBolt(id);
        if (bolt == null) return;
        _bolts.Remove(bolt);
        bolt.DestroySelf();
    }

    public void ClearBolt()
    {
        foreach (var bolt in _bolts.ToArray())
        {
            _bolts.Remove(bolt);
            bolt.DestroySelf();
        }
        _bolts.Clear();
        _id = 0;
    }

    public IEnumerable<T> GetBolts()
    {
        return _bolts.ToArray();
    }
}
