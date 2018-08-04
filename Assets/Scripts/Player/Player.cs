using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerMovement movement;
    public PlayerSpawner spawner;

    public void Initialize()
    {
        //movement = FindObjectOfType<PlayerMovement>();
        movement.Initialize();
    }
}

