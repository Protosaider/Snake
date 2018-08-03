using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSebLag : MonoBehaviour {

    public GameObject prefab;

    public float delta = 1.2f;

    private float nextSpawn = 1f;

    // Use this for initialization
    void Start ()
    {
        PoolManagerSeb.instance.CreatePool(prefab, 10);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (nextSpawn <= Time.time)
        {
            PoolManagerSeb.instance.ReuseObject(prefab, transform.position, Quaternion.identity);

            nextSpawn = Time.time + delta;
        }

    }
}
