using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomObjectPooling
{
    class PooledObject : MonoBehaviour
    {
        [System.NonSerialized]
        ObjectPool referenceToPoolInstance;

        #region Pool Parameters
        [SerializeField]
        private int initialPoolSize = 100;
        public int InitialPoolSize { get { return initialPoolSize; } }

        public int enlargeBy = 10;
        public bool canEnlarge = true;

        public bool useDontDestroyOnLoad = false;

        public bool setAsPoolChild = true;
        #endregion

        public event System.Action OnGetEvent;
        public event System.Action OnReturnEvent;

        protected virtual void OnEnable()
        {
            //!print("I'm enabled");

            if (OnGetEvent != null)
                OnGetEvent();
        }

        protected virtual void OnDisable()
        {
            //!print("I'm disabled");

            if (OnReturnEvent != null)
                OnReturnEvent();
        }

        public T Get<T>(bool enable = true) where T : PooledObject
        {
            var pool = ObjectPool.GetPool(this);
            var pooledObject = pool.Get<T>();

            if (enable)
            {
                pooledObject.gameObject.SetActive(true);
            }

            return pooledObject;
        }

        public T Get<T>(Transform parent, bool resetTransform = false) where T : PooledObject
        {
            var pooledObject = Get<T>(true);
            pooledObject.transform.SetParent(parent);

            if (resetTransform)
            {
                pooledObject.transform.localPosition = Vector3.zero;
                pooledObject.transform.localRotation = Quaternion.identity;
            }

            return pooledObject;
        }

        public T Get<T>(Transform parent, Vector3 relativePosition, Quaternion relativeRotation) where T : PooledObject
        {
            var pooledObject = Get<T>(true);
            pooledObject.transform.SetParent(parent);

            pooledObject.transform.localPosition = relativePosition;
            pooledObject.transform.localRotation = relativeRotation;

            return pooledObject;
        }
    }
}


