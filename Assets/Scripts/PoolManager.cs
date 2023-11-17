using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    Dictionary<string, List<GameObject>> _pool;
    Transform _poolParent;

    private void Awake()
    {
        _pool = new Dictionary<string, List<GameObject>>();
        _poolParent = new GameObject("Pool Parent").transform;
    }

    private void LoadInternal(GameObject prefab, int quantity = 1)
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
            go.SetActive(false);
            _pool[go.name].Add(go);
        }
    }

    private GameObject SpawnInternal(GameObject prefab)
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

    private void DespawnInternal(GameObject go)
    {
        if (!_pool.ContainsKey(go.name))
        {
            _pool[go.name] = new List<GameObject>();
        }
        go.SetActive(false);
        go.transform.SetParent(_poolParent, false);
        _pool[go.name].Add(go);
    }

    public static void Load(GameObject prefab, int quantity = 1)
    {
        instance.LoadInternal(prefab, quantity);
    }

    public static GameObject Spawn(GameObject prefab)
    {
        return instance.SpawnInternal(prefab);
    }

    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var go = instance.SpawnInternal(prefab);
        var t = go.transform;
        t.position = position;
        t.rotation = rotation;
        return go;
    }

    public static void Despawn(GameObject go)
    {
        instance.DespawnInternal(go);
    }

}