using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : PooledObjectSimple
{
    public float speed = 5.0f;

    private void OnEnable()
    {
        print("I'm enabled");
        //Invoke("Destroy", 2.0f);
    }

    private void Destroy()
    {
        //pool.ReturnObject(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(speed * Vector3.forward * Time.deltaTime);
    }
}
