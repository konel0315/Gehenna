using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Gehenna
{
    public class PoolingManager : ISubManager
    {
        private PoolingParam param;
        private readonly Dictionary<string, ObjectPool<GameObject>> monoPoolDict = new();
		private readonly Dictionary<Type, object> objectPoolDict = new();

        public void Initialize(ManagerParam param)
        {
            if (param is not PoolingParam poolingParam)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.param = poolingParam;
            
            foreach (var catalog in param.GameConfig.GetAllCatalogs())
            {
                if (catalog is IPoolableCatalog poolableCatalog)
                {
                    IEnumerable<IPoolableEntry> poolableEntries = poolableCatalog.GetPoolableEntries();
                    if (poolableEntries == null)
                        continue;

                    GameObject parent = new GameObject($"POOL_{catalog.name}");
                    foreach (var each in poolableEntries)
                    {
                        PoolableBundle poolableBundle = each.Bundle;
                        if (poolableBundle.IsPoolable)
                        {
                            CreateMonoPool
                            (
                                key: each.Key, 
                                prefab: poolableBundle.Prefab, 
                                capacity: poolableBundle.Capacity, 
                                parent.transform
                            );
                        }
                    }
                }
            }
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            monoPoolDict.Clear();
            objectPoolDict.Clear();
        }
        
        public void ManualUpdate(float deltaTime) { }
        public void ManualFixedUpdate() { }
        
        public GameObject GetMono(string key)
        {
            if (!monoPoolDict.TryGetValue(key, out var pool))
                throw new KeyNotFoundException($"The prefab for key {key} could not be found.");

            return pool.Get();
        }
        
        public T GetObject<T>() where T : class
        {
            if (!objectPoolDict.TryGetValue(typeof(T), out var poolObj))
                throw new KeyNotFoundException($"The object {typeof(T).Name} could not be found.");

            var pool = poolObj as ObjectPool<T>;
            if (pool == null)
                throw new InvalidCastException($"Stored pool is not of type ObjectPool<{typeof(T).Name}>.");

            return pool.Get();
        }
        
        public void ReleaseMono(string key, GameObject instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            if (!monoPoolDict.TryGetValue(key, out var pool))
                throw new KeyNotFoundException($"The prefab pool for key {key} could not be found.");

            pool.Release(instance.gameObject);
        }
        
        public void ReleaseObject<T>(T instance) where T : class
        {
            if (!objectPoolDict.TryGetValue(typeof(T), out var poolObj))
                throw new KeyNotFoundException($"The object {typeof(T).Name} could not be found.");

            var pool = poolObj as ObjectPool<T>;
            if (pool == null)
                throw new InvalidCastException($"Stored pool is not of type ObjectPool<{typeof(T).Name}>.");

            pool.Release(instance);
        }
        
        private void CreateMonoPool(string key, GameObject prefab, int capacity, Transform poolRoot)
        {
            if (string.IsNullOrEmpty(key))
            {
                GehennaLogger.Log(this, LogType.Error, "Key is invalid");
                return;
            }
            
            if (monoPoolDict.ContainsKey(key))
            {
                GehennaLogger.Log(this, LogType.Warning, $"Pool already exists for type {key.ToString()}");
                return;
            }

            ObjectPool<GameObject> pool = new ObjectPool<GameObject>
            (
                createFunc: () =>
                {
                    return UnityEngine.Object.Instantiate(prefab, poolRoot);
                },
                actionOnGet: go =>
                {
                    go.SetActive(true);
                },
                actionOnRelease: go =>
                {
                    go.SetActive(false);
                    go.transform.SetParent(poolRoot, worldPositionStays: true);
                },
                actionOnDestroy: go =>
                {
                    UnityEngine.Object.Destroy(go);
                },
                defaultCapacity: capacity
            );

            monoPoolDict.Add(key, pool);
            PrewarmPool(pool, capacity);

            GehennaLogger.Log(this, LogType.Success, $"Prefab pool created for: {prefab.name} (Capacity: {capacity})");
        }
        
        public void CreateObjectPool<T>(Func<T> factory, int capacity) where T : class
        {
            if (factory == null)
            {
                GehennaLogger.Log(this, LogType.Error, "Factory is null");
                return;
            }
            
            if (objectPoolDict.ContainsKey(typeof(T)))
            {                
                GehennaLogger.Log(this, LogType.Warning, $"Pool already exists for type {typeof(T).Name}");
                return;
            }

            ObjectPool<T> pool = new ObjectPool<T>
            (
                createFunc: () =>
                {
                    return factory();
                },
                defaultCapacity: capacity
            );
            
            objectPoolDict.Add(typeof(T), pool);
            PrewarmPool(pool, capacity);
            
            GehennaLogger.Log(this, LogType.Success, $"Object pool created for: {typeof(T).Name} (Capacity: {capacity})");
        }
        
        private void PrewarmPool<T>(ObjectPool<T> pool, int count) where T : class
        {
            List<T> temp = new(count);
            for (int i = 0; i < count; i++)
                temp.Add(pool.Get());

            foreach (var obj in temp)
                pool.Release(obj);
        }
    }
}
