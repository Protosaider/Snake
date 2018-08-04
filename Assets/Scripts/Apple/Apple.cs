using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {

    public NodeGrid nodeGrid;

    public Apple[] apples;

    public float timeOfLife = 3.0f;

    private float deathTime;

    // Update is called once per frame
    void Update()
    {
        if (deathTime < Time.time)
        {
            Debug.Log("Apple is alive");
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start ()
    {
        deathTime = Time.time + timeOfLife;
    }

    void HasPickedUp()
    {

        Destroy(this);
    }
	
}
