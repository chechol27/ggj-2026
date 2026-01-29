using System;
using UnityEngine;

enum SpawnerState
{
    Ready,
    Preloading
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private bool preload;
    [SerializeField][Range(0,256)] private int maxSize;
    [SerializeField] private int batchSize;

    [SerializeField] private SpawnerState state;

    private void Awake()
    {
        if (preload)
        {
            state = SpawnerState.Preloading;
        }
    }

    protected virtual void Update()
    {
        if (state == SpawnerState.Preloading)
        {
            int instanceCount = GameServices.Get<Pool>().GetInstanceCount(prefab);
            if (instanceCount >= maxSize)
            {
                state = SpawnerState.Ready;
            }

            GameServices.Get<Pool>().RequestPreload(prefab, batchSize);
        }
    }

    protected virtual Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Spawn()
    {
        if (state != SpawnerState.Ready) return;
        GameServices.Get<Pool>().Spawn(prefab, out GameObject _, TransformFrame.T(GetPosition()));
    }
}
