using UnityEngine;

public class ManagersLoader : MonoBehaviour {

    public InputManager inputManager;         //prefab to instantiate.

    void Start()
    {
        //! Check if a InputManager has already been assigned to static variable GameManager.instance or if it's still null
        if (InputManager.instance == null)
            //! Instantiate InputManager prefab
            Instantiate(inputManager);
    }
}