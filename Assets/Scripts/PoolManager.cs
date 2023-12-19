using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    // Diccionario que almacena las instancias de objetos por nombre.
    Dictionary<string, List<GameObject>> _pool;
    Transform _poolParent;

    private void Awake()
    {
        // Inicializa el diccionario y el objeto padre para las instancias.
        _pool = new Dictionary<string, List<GameObject>>();
        _poolParent = new GameObject("Pool Parent").transform;
    }

    // Carga instancias del prefab en la piscina.
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

    // Obtiene una instancia del prefab desde la piscina.
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

    // Desactiva y devuelve una instancia a la piscina.
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

    // Método para cargar instancias en la piscina.
    public static void Load(GameObject prefab, int quantity = 1)
    {
        instance.LoadInternal(prefab, quantity);
    }

    // Método para obtener una instancia desde la piscina.
    public static GameObject Spawn(GameObject prefab)
    {
        return instance.SpawnInternal(prefab);
    }

    // Método para obtener una instancia desde la piscina con posición y rotación.
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var go = instance.SpawnInternal(prefab);
        var t = go.transform;
        t.position = position;
        t.rotation = rotation;
        return go;
    }

    // Método para devolver una instancia a la piscina.
    public static void Despawn(GameObject go)
    {
        instance.DespawnInternal(go);
    }

    // Método para obtener la cantidad de instancias de un prefab en la piscina.
    public static int GetInstanceCount(GameObject prefab)
    {
        if (instance._pool.ContainsKey(prefab.name))
        {
            return instance._pool[prefab.name].Count;
        }
        return 0;
    }
}
