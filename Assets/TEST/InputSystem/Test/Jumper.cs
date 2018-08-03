using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour {

    public delegate void JumpDelegate();
    public event JumpDelegate JumpEvent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(JumpEvent.GetInvocationList().Length);

        if (InputManager.instance.GetButtonDown(0, EInputAction.Jump))
        {
            if (JumpEvent != null)
            {
                JumpEvent.Invoke();
            }
        }
	}
}
