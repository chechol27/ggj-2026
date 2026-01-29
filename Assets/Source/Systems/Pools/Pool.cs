using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour, IGameService
{
    private const int MAX_OBJECT_COUNT = 256;
    
    private readonly Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject, List<GameObject>>();
    
    public bool Spawn(GameObject prefab,out GameObject result,bool activate = true)
    {
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new List<GameObject>();
        }
        result = pools[prefab].FirstOrDefault(go => !go.activeSelf);
        if (result == null)
        {
            if (pools[prefab].Count == MAX_OBJECT_COUNT)
                return false;
            result = Instantiate(prefab);
            result.SetActive(false);
            pools[prefab].Add(result);
        }
        result.SetActive(activate);
        return result;
    }

    public bool Spawn<TComponent>(GameObject prefab, out TComponent result, bool activate = true) where TComponent : Component
    {
        if (prefab.TryGetComponent(out TComponent _))
        {
            if (Spawn(prefab, out result, activate))
            {
                return result.GetComponent<TComponent>();
            }

            return false;
        }

        throw new ArgumentException($"{prefab} prefab doesnt contain a {typeof(TComponent)} component");
    }

    public bool Spawn(GameObject prefab, out GameObject result, TransformFrame frame, bool activate = true)
    {
        if (Spawn(prefab, out result, activate))
        {
            result.transform.position = frame.position;
            result.transform.rotation = frame.rotation;
            result.transform.localScale = frame.scale;
            return true;
        }

        return false;
    }

    public bool Spawn<TComponent>(GameObject prefab, out TComponent result, TransformFrame frame, bool activate = true) where TComponent : Component
    {
        if (Spawn<TComponent>(prefab, out result, activate))
        {
            result.transform.position = frame.position;
            result.transform.rotation = frame.rotation;
            result.transform.localScale = frame.scale;
            return true;
        }

        return false;
    }

    public int GetInstanceCount(GameObject prefab)
    {
        return pools.TryGetValue(prefab, out List<GameObject> pool) ? pool.Count : 0;
    }

    public void RequestPreload(GameObject prefab, int objectCount)
    {
        if(!pools.ContainsKey(prefab))
            pools[prefab] = new List<GameObject>();

        var list = pools[prefab];
        for (int i = 0; i < objectCount; i++)
        {
            GameObject go = Instantiate(prefab);
            list.Add(go);
            go.SetActive(false);
        }
    }
}
