using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [System.Serializable]
    public class ObjectPool
    {
        #region PoolParameters

        [SerializeField]
        public Transform poolHolder;

        /// <summary>
        /// Prefab of objects in pool.
        /// </summary>
        [SerializeField]
        public PooledObject prefab;

        /// <summary>
        /// Amount of objects in pool. Can be increased if pool attribute canExpand == true.
        /// </summary>
        [SerializeField]
        public int poolSize;

        /// <summary>
        /// Attribute that shows pool's possibility to add new objects if they are required.
        /// </summary>
        [SerializeField]
        public bool canExpand;
        #endregion

        /// <summary>
        /// Pool's container. It's a Queue, so it can't be serialized.
        /// </summary>
        private Queue<PooledObject> pool;

        #if UNITY_EDITOR
                public List<PooledObject> listShowPool;
        #endif

        //public ObjectPool()
        //{
        //}

        public ObjectPool(PooledObject prefab, int poolSize = 0, bool canExpand = true, Transform poolHolder = null)
        {
            this.poolHolder = poolHolder;
            this.prefab = prefab;
            this.poolSize = Mathf.Max(2, poolSize);
            this.canExpand = canExpand;

            if (poolSize == 0)
            {
                pool = new Queue<PooledObject>();
            }
            else
            {
                pool = new Queue<PooledObject>(this.poolSize);
            }

            #if UNITY_EDITOR
                        listShowPool = new List<PooledObject>(pool);
            #endif
        }

        public ObjectPool(PoolParams poolParams)
        {
            this.prefab = poolParams.prefab;
            this.poolSize = Mathf.Max(2, poolParams.poolSize);
            this.canExpand = poolParams.canExpand;
            this.poolHolder = poolParams.poolHolder;
            if (poolSize == 0)
            {
                pool = new Queue<PooledObject>();
            }
            else
            {
                pool = new Queue<PooledObject>(this.poolSize);
            }

            #if UNITY_EDITOR
                        listShowPool = new List<PooledObject>(pool);
            #endif
        }

        public void Enqueue(PooledObject gameObject)
        {
            this.poolSize++;
            pool.Enqueue(gameObject);
            #if UNITY_EDITOR
                        listShowPool = new List<PooledObject>(pool);
            #endif
        }

        public PooledObject Dequeue()
        {
            if (pool.Peek().IsActive())
            {
                return null;
            }

            this.poolSize--;
            #if UNITY_EDITOR
                        listShowPool = new List<PooledObject>(pool);
            #endif
            return pool.Dequeue();
        }

        public virtual void ReuseObject()
        {
            PooledObject objectToReuse = pool.Dequeue();
            pool.Enqueue(objectToReuse);
            objectToReuse.Reuse();

            #if UNITY_EDITOR
                        listShowPool = new List<PooledObject>(pool);
            #endif
        }

    }
}