using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{ 

    public class ObjectPoolsManager : MonoBehaviour {

        /// <summary>
        /// Contains pools. Key/Value: <c>{KeyValuePair{int, string}, ObjectPool}</c>,
        /// where <c>int</c> == prefabInstanceID and <c>int</c> == prefabTag.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        ///     <item>
        ///         <term>prefabInstanceID</term>
        ///         <description>Key of pool that also are prefab instance ID.</description>
        ///     </item>
        ///     <item>
        ///         <term>prefabTag</term>
        ///         <description>Tag of objects in pool.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        private Dictionary<KeyValuePair<int, string>, ObjectPool> poolDictionary = new Dictionary<KeyValuePair<int, string>, ObjectPool>();

        /// <summary>
        /// List of pools parameters that can be changed in editor. Pools objects with this parameters will be generated on game start.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        private List<PoolParams> preGenParamsForPools = new List<PoolParams>();
        [UnityEngine.Serialization.FormerlySerializedAs("preGenParamsForPools")]
        [SerializeField]
        private List<PoolParams> parametersForPoolsGeneration;

        /// <summary>
        /// List of dictionary keys.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        private List<KeyValuePair<int, string>> dictionaryKeys = new List<KeyValuePair<int, string>>();

        public PooledObject cube;

        private void Start()
        {
            PoolsFromEditorInitialization();
        }

        private void PoolsFromEditorInitialization()
        {
            for (int i = 0; i < preGenParamsForPools.Count; i++)
            {
                PoolParams poolParams = preGenParamsForPools[i];

                if (poolParams.poolHolder == null)
                {
                    GameObject poolHolderObject = new GameObject(poolParams.prefab.name + " Objects Pool");
                    poolHolderObject.transform.parent = transform;
                    poolParams.poolHolder = poolHolderObject.transform;
                }

                CreatePool(poolParams.prefab, poolParams.poolSize, poolParams.canExpand, poolParams.poolHolder);
            }
        }

        #region public void CreatePool()

        public void CreatePool(PoolParams poolParams)
        {
            KeyValuePair<int, string> poolKey = DictionaryKey(poolParams.prefab);

            if (!poolDictionary.ContainsKey(poolKey))
            {
                dictionaryKeys.Add(poolKey);

                if (poolParams.poolHolder == null)
                {
                    GameObject poolHolderObject = new GameObject(poolParams.prefab.name + " Objects Pool");
                    poolHolderObject.transform.parent = transform;
                    poolParams.poolHolder = poolHolderObject.transform;
                }

                poolDictionary.Add(poolKey, new ObjectPool(poolParams));             

                for (int i = 0; i < poolParams.poolSize; i++)
                {
                    AddObject(poolKey, poolParams.prefab);
                }
            }
        }

        public void CreatePool(PooledObject prefab, int poolSize, bool canExpand = true, Transform poolHolder = null)
        {
            KeyValuePair<int, string> poolKey = DictionaryKey(prefab);

            if (!poolDictionary.ContainsKey(poolKey))
            {
                dictionaryKeys.Add(poolKey);

                if (poolHolder == null)
                {
                    GameObject poolHolderObject = new GameObject(prefab.name + " Objects Pool");
                    poolHolderObject.transform.parent = transform;
                    poolHolder = poolHolderObject.transform;
                }

                poolDictionary.Add(poolKey, new ObjectPool(prefab, poolSize, canExpand, poolHolder));

                for (int i = 0; i < poolSize; i++)
                {
                    AddObject(poolKey, prefab);
                }
            }
        }
        #endregion

        #region public void ReuseObject()

        public void ReuseObject(PooledObject prefab)
        {
            KeyValuePair<int, string> poolKey = DictionaryKey(prefab);

            if (poolDictionary.ContainsKey(poolKey))
            {
                PooledObject objectToReuse = poolDictionary[poolKey].Dequeue();
                if (objectToReuse == null)
                {
                    objectToReuse = Instantiate(prefab) as PooledObject;
                    poolDictionary[poolKey].Enqueue(objectToReuse);
                   
                }
                poolDictionary[poolKey].Enqueue(objectToReuse);

                objectToReuse.Reuse();
            }
        }

        public void ReuseObject(int prefabInstanceID)
        {
            KeyValuePair<int, string> poolKey = DictionaryKey(prefabInstanceID);

            if (IsDictionaryKeyFound(poolKey))
            {
                PooledObject objectToReuse = poolDictionary[poolKey].Dequeue();
                poolDictionary[poolKey].Enqueue(objectToReuse);

                objectToReuse.Reuse();
            }
        }

        public void ReuseObject(string prefabTag)
        {
            KeyValuePair<int, string> poolKey = DictionaryKey(prefabTag);

            if (IsDictionaryKeyFound(poolKey))
            {
                PooledObject objectToReuse = poolDictionary[poolKey].Dequeue();
                poolDictionary[poolKey].Enqueue(objectToReuse);

                objectToReuse.Reuse();
            }
        }
        #endregion

        #region private KeyValuePair<int, string> DictionaryKey()

        private KeyValuePair<int, string> DictionaryKey(PooledObject prefab)
        {
            return new KeyValuePair<int, string>(prefab.GetInstanceID(), prefab.tag);
        }

        private KeyValuePair<int, string> DictionaryKey(int prefabInstanceID)
        {
            foreach (KeyValuePair<int, string> dictionaryKey in dictionaryKeys)
            {
                if (dictionaryKey.Key == prefabInstanceID)
                {
                    return dictionaryKey;
                }
            }

            Debug.LogWarning("Pools Manager hasn't pool with prefabInstanceID == " + prefabInstanceID);
            return new KeyValuePair<int, string>(-1, "");
        }

        private KeyValuePair<int, string> DictionaryKey(string prefabTag)
        {
            foreach (KeyValuePair<int, string> dictionaryKey in dictionaryKeys)
            {
                if (dictionaryKey.Value == prefabTag)
                {
                    return dictionaryKey;
                }
            }

            Debug.LogWarning("Pools Manager hasn't pool with prefabTag == " + prefabTag);
            return new KeyValuePair<int, string>(-1, "");
        }
        #endregion

        private void AddObject(KeyValuePair<int, string> poolKey, PooledObject prefab)
        {
            PooledObject newObject = Instantiate(prefab) as PooledObject;
            poolDictionary[poolKey].Enqueue(newObject);
            newObject.SetParent(poolDictionary[poolKey].poolHolder);
        }

        private bool IsDictionaryKeyFound(KeyValuePair<int, string> pair)
        {
            //return (pair.Key == -1 && pair.Value == "") ? false : true;
            return (pair.Value == "") ? false : true;
        }

        //private void Update()
        //{
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        CreatePool(cube, 10, true, transform);
        //    }
        //}
    }
}

