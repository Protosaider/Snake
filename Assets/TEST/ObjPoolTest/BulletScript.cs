﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float speed = 5.0f;

	// Update is called once per frame
	void Update () {
        
        transform.Translate(speed * Vector3.forward * Time.deltaTime);
	}
}
