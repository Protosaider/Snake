using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    private IInputManager input;

	// Use this for initialization
	void Start () {
        input = InputManager.instance; //static getter
    }
	
	// Update is called once per frame
	void Update () {
        //InputManager.instance.GetButtonDown(0, EInputAction.Jump);
    }

    private void OnJump()
    {
        transform.position += Vector3.up;
    }

    private void OnEnable()
    {
        FindObjectOfType<Jumper>().JumpEvent += OnJump;
    }

    private void OnDisable()
    {
        FindObjectOfType<Jumper>().JumpEvent -= OnJump;
    }
}
