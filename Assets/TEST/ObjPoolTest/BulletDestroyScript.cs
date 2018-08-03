using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyScript : MonoBehaviour {

    // Use this for initialization
    private void OnEnable () {
        //print("I'm enabled");
        Invoke("Destroy", 2.0f);
	}

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
       // print("I'm disabled");
    }

}
