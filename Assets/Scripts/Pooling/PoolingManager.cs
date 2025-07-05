using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Gehenna
{
    public class PoolingManager : ISubManager
    {
        private PoolingContext  context;
        private readonly Dictionary<Enum, ObjectPool<GameObject>> prefabPoolDict = new();
		private readonly Dictionary<Type, object> objectPoolDict = new();

        public void Initialize(ManagerContext context)
        {
            if (context is not PoolingContext poolingContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.context = poolingContext;

            foreach (var catalog in context.GameConfig.GetAllCatalogs())
            {
                if (catalog is IPoolableCatalog poolableCatalog)
                {
                    var poolableEntries = poolableCatalog.GetPoolableEntries();
                    if (poolableEntries == null)
                        continue;

                    GameObject parent = new GameObject($"[ROOT_{catalog.GetType().Name}]");

                    foreach (var entry in poolableEntries)
                    {
                        if (entry.IsPooling)
                        {
                            CreatePrefabPool(entry.Key, entry.Prefab, entry.Capacity, parent.transform);
                        }
                    }
                }
            }
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            prefabPoolDict.Clear();
            objectPoolDict.Clear();
        }
        public GameObject GetPrefabInstance(Enum key)
        {
            if (!prefabPoolDict.TryGetValue(key, out var pool))
                throw new KeyNotFoundException($"The prefab for key {key} could not be found.");

            return pool.Get();
        }
        
        public void ReleasePrefabInstance(Enum key, GameObject instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            if (!prefabPoolDict.TryGetValue(key, out var pool))
                throw new KeyNotFoundException($"The prefab pool for key {key} could not be found.");

            pool.Release(instance);
        }

        public T GetObjectInstance<T>() where T : class
        {
            if (!objectPoolDict.TryGetValue(typeof(T), out var poolObj))
                throw new KeyNotFoundException($"The object {typeof(T).Name} could not be found.");

            var pool = poolObj as ObjectPool<T>;
            if (pool == null)
                throw new InvalidCastException($"Stored pool is not of type ObjectPool<{typeof(T).Name}>.");

            return pool.Get();
        }
        
        public void ReleaseObjectInstance<T>(T obj) where T : class
        {
            if (!objectPoolDict.TryGetValue(typeof(T), out var poolObj))
                throw new KeyNotFoundException($"The object {typeof(T).Name} could not be found.");

            var pool = poolObj as ObjectPool<T>;
            if (pool == null)
                throw new InvalidCastException($"Stored pool is not of type ObjectPool<{typeof(T).Name}>.");

            pool.Release(obj);
        }
        
        public void CreateObjectPool<T>(Func<T> factory, int capacity) where T : class
        {
            if (objectPoolDict.ContainsKey(typeof(T)))
                return;

            var pool = new ObjectPool<T>
            (
                createFunc: () => factory != null ? factory() : Activator.CreateInstance<T>(),
                defaultCapacity: capacity
            );

            objectPoolDict.Add(typeof(T), pool);
            GehennaLogger.Log(this, LogType.Success, $"Object pool created for: {typeof(T).Name} (Capacity: {capacity})");
        }

        private void CreatePrefabPool(Enum key, GameObject prefab, int capacity, Transform parent)
        {
            if (prefabPoolDict.ContainsKey(key))
                return;

            var pool = new ObjectPool<GameObject>
            (
                createFunc: () => UnityEngine.Object.Instantiate(prefab, parent),
                actionOnGet: go => go.SetActive(true),
                actionOnRelease: go =>
                {
                    go.SetActive(false);
                    go.transform.SetParent(parent, worldPositionStays: true);
                },
                actionOnDestroy: go => UnityEngine.Object.Destroy(go),
                defaultCapacity: capacity
            );

            prefabPoolDict.Add(key, pool);

            PrewarmPool(pool, capacity);

            GehennaLogger.Log(this, LogType.Success, $"Prefab pool created for: {prefab.name} (Capacity: {capacity})");

        }
        
        private void PrewarmPool<T>(ObjectPool<T> pool, int count) where T : class
        {
            List<T> temp = new(count);
            for (int i = 0; i < count; i++)
                temp.Add(pool.Get());

            foreach (var obj in temp)
                pool.Release(obj);
        }
        
        public void ManualUpdate() { }
        public void ManualLateUpdate() { }
        public void ManualFixedUpdate() { }
    }
}
