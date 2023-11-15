using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, List<GameObject>> _pool;
    private Transform _poolParent;

    private void Awake()
    {
        _poolParent = new GameObject("Pool Parent").transform;
    }

    public void Load(GameObject prefab, int quantity = 1)
    {
        var goName = prefab.name;

        if (!_pool.ContainsKey(goName))
        {
            _pool[goName] = new List<GameObject>();
        }

        for (int i = 0; i < quantity; i++)
          {
            var go = Instantiate(prefab, _poolParent, false);

            go.name = goName;
            go.transform.SetParent(_poolParent, false);
            go.SetActive(false);
            _pool[go.name].Add(go);
        }
    }

    public GameObject Spawn(GameObject prefab)
    {
        if (!_pool.ContainsKey(prefab.name) || _pool[prefab.name].Count == 0)
        {
            Load(prefab);
        }

        var l = _pool[prefab.name];
        var go = l[0];

        l.RemoveAt(0);
        go.SetActive(true);
        go.transform.SetParent(null, false);

        return go;
    }

    public void Despawn(GameObject go)
    {
        if (!_pool.ContainsKey(go.name))
        {
            _pool[go.name] = new List<GameObject>();
        }

        go.SetActive(false);
        go.transform.SetParent(_poolParent, false);
        _pool[go.name].Add(go);
    }
}
