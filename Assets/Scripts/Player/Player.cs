using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerSettings settings;

    public GameObject head;
    public GameObject body;
    public GameObject connection;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    private void Initialize()
    {
        ObjectSpawner spawner = new ObjectSpawner();
        spawner.SpawnObjectFromNode(head, settings.spawnX, settings.spawnZ);
    }

    public void SpawnHead(int x, int z)
    {

    }

    public void ChangeSnakeSize()
    {

    }
}
