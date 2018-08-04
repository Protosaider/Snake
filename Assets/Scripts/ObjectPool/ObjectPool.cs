using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomObjectPooling
{
    class ObjectPool : MonoBehaviour
    {
        private static Dictionary<PooledObject, ObjectPool> pools = new Dictionary<PooledObject, ObjectPool>();

        PooledObject prefab;

        private Queue<PooledObject> objects = new Queue<PooledObject>();

        //private List<PooledObject> disabledObjects = null;

#if UNITY_EDITOR
        public List<PooledObject> listShowPool;

        private void RenewShowList()
        {
            listShowPool = new List<PooledObject>(objects);
        }
#endif

        private ObjectPool() { }

        public static ObjectPool GetPool(PooledObject prefab, bool setPoolAsChild = false)
        {
            if (pools.ContainsKey(prefab))
                return pools[prefab];

            // creates separated object pool
            //GameObject obj = new GameObject("Pool-" + (prefab as Component).name);
            //var pool = obj.AddComponent<ObjectPool>();

            var pool = new GameObject("Pool-" + (prefab as Component).name).AddComponent<ObjectPool>();

            pool.prefab = prefab;

            pool.EnlargePool(prefab.InitialPoolSize);

            if (prefab.useDontDestroyOnLoad)
            {
                DontDestroyOnLoad(pool.gameObject);
            }

            //pool.disabledObjects = new List<PooledObject>();       
            
            pools.Add(prefab, pool);
            return pool;
        }

        public T Get<T>() where T : PooledObject
        {
            if (objects.Count == 0)
            {
                if (prefab.canEnlarge)
                {
                    EnlargePool(prefab.enlargeBy);
                }
            }

            //Debug.Log("COUNT EN " + objects.Count);
            //Debug.Log("COUNT DIS " + disabledObjects.Count);
            //Debug.Log("COUNT pools " + pools.Count);

            if (objects.Count > 0)
            {
                var pooledObject = objects.Dequeue();
#if UNITY_EDITOR
                RenewShowList();
#endif
                return pooledObject as T;
            }
            else
            {
                return null;
            }

            //objects.Enqueue(pooledObject);

            //as - if cannot cast  - returns null
            //return pooledObject as T;
        }

        private void EnlargePool(int additionalCapacity)
        {
            for (int i = 0; i < additionalCapacity; i++)
            {
                var pooledObject = Instantiate(this.prefab) as PooledObject;
                (pooledObject as Component).gameObject.name += " " + i;

                //! on enable
                //pooledObject.OnGetEvent += () => GetObjectFromPool(pooledObject);

                //pooledObject.OnReturnEvent += () => AddObjectToAvailable(pooledObject);
                pooledObject.OnReturnEvent += () => ReturnObjectToPool(pooledObject);

                (pooledObject as Component).gameObject.SetActive(false);
                //! test
                if (this.prefab.setAsPoolChild)
                {
                    (pooledObject as Component).gameObject.transform.SetParent(this.gameObject.transform);
                }
            }

#if UNITY_EDITOR
            RenewShowList();
#endif
        }

        private void GetObjectFromPool(PooledObject pooledObject)
        {
            //if (prefab.setAsPoolChild)
            //{
            //    (pooledObject as Component).gameObject.transform.SetParent(gameObject.transform);
            //}
        }

        private void ReturnObjectToPool(PooledObject pooledObject)
        {
            //disabledObjects.Add(pooledObject);
            objects.Enqueue(pooledObject);

            //pooledObject.transform

            //! not workings because (written below)
            //if (prefab.setAsPoolChild)
            //{
            //    (pooledObject as Component).gameObject.transform.SetParent(gameObject.transform);
            //}

#if UNITY_EDITOR
            RenewShowList();
#endif
        }

        //! It's Unity: you cannot parent object if this object is disabled in the same frame.

        //private void Update()
        //{
        //    MakeDisabledObjectsChildren();           
        //}
        //private void MakeDisabledObjectsChildren()
        //{
        //    if (disabledObjects.Count > 0)
        //    {
        //        foreach (var pooledObject in disabledObjects)
        //        {
        //            if (pooledObject.gameObject.activeInHierarchy == false)
        //            {
        //                (pooledObject as Component).transform.SetParent(transform);
        //            }
        //        }

        //        disabledObjects.Clear();
        //    }
        //}

        //private void AddObjectToAvailable(PooledObject pooledObject)
        //{
        //    //disabledObjects.Add(pooledObject);
        //    enabledObjects.Enqueue(pooledObject);
        //}
    }
}

