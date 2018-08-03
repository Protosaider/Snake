using UnityEngine;

namespace ObjectPooling
{
    [System.Serializable]
    public class PooledObject : MonoBehaviour, IPoolObject
    {

        //After SetActive(false)
        protected virtual void Start() { }

        //After SetActive(false)
        protected virtual void OnDisable() { }

        //After SetActive(true). Also works when object is instantiated.
        protected virtual void OnEnable()
        {
            gameObject.SetActive(false);
        }

        //This method is suggested to reactivate object
        public virtual void Reuse()
        {
            gameObject.SetActive(true);
        }

        public bool IsActive()
        {
            return gameObject.activeInHierarchy;
        }

        //This method is suggested to be done after Reuse() method invoked.
        public virtual void OnReuse() { }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }
    }
}