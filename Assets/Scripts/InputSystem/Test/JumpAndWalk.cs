using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAndWalk : MonoBehaviour {

    private ControllersInputsEvents inputsEvents;

    private bool isAssigned = false;

    // Use this for initialization
    private void Awake ()
    {
        //inputsEvents = FindObjectOfType<ControllersInputsEvents>();
    }

    private void Update()
    {
        if (inputsEvents == null)
        {
            Debug.Log("Is Null");
            inputsEvents = FindObjectOfType<ControllersInputsEvents>();          
        }

        if (inputsEvents != null && !isAssigned)
        {
            inputsEvents.ActionJumpEvent += OnJump;
            inputsEvents.ActionHorizontalValueEvent += OnHorizontal;
            isAssigned = true;
        }
    }

    private void OnEnable()
    {
        //inputsEvents.ActionJumpEvent += OnJump;
        //inputsEvents.ActionHorizontalValueEvent += OnHorizontal;
    }

    private void OnJump()
    {
        transform.position += Vector3.up;
    }

    private void OnHorizontal(float value)
    {
        transform.position += new Vector3(value, 0.0f, 0.0f);
    }

    private void OnDisable()
    {
        inputsEvents.ActionJumpEvent -= OnJump;
        inputsEvents.ActionHorizontalValueEvent += OnHorizontal;
    }
}
