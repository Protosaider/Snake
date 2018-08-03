namespace ObjectPooling
{

    /// <summary>
    /// Pool parameters that can be changed in UnityEditor. Defines pool's prefab, size and ability to expand.
    /// </summary>
    [System.Serializable]
    public struct PoolParams
    {  
        /// <summary>
        /// Prefab of objects in pool.
        /// </summary>
        public PooledObject prefab;

        /// <summary>
        /// Amount of objects in pool. Can be increased if pool attribute canExpand == true.
        /// </summary>
        public int poolSize;

        /// <summary>
        /// Attribute that shows pool's possibility to add new objects if they are required.
        /// </summary>
        public bool canExpand;

        /// <summary>
        /// 
        /// </summary>
        public UnityEngine.Transform poolHolder;
    }
}
