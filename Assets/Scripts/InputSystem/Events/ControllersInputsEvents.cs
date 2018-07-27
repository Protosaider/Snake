using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersInputsEvents : MonoBehaviour {

    private ControllersInputsSystem inputsSystem;


    public delegate void actionStatusDelegate();

    public event actionStatusDelegate ActionJumpEvent;
    public event actionStatusDelegate ActionHorizontalEvent;


    public delegate void actionValueDelegate(float value);

    public event actionValueDelegate ActionHorizontalValueEvent;

    private void Awake()
    {
        inputsSystem = gameObject.AddComponent<ControllersInputsSystem>();
    }

    // Update is called once per frame
    void Update ()
    {
        float horizontal = inputsSystem.Horizontal(0);

        if (!Mathf.Approximately(0.0f, horizontal))
        {
            if (ActionHorizontalEvent != null)
            {
                ActionHorizontalEvent.Invoke();
            }

            if (ActionHorizontalValueEvent != null)
            {
                ActionHorizontalValueEvent.Invoke(horizontal);
            }
            Debug.Log("Fired Horizontal");
        }

        if (inputsSystem.IsDown(0, EInputAction.Jump))
        {
            if (ActionJumpEvent != null)
            {
                ActionJumpEvent.Invoke();
            }
            Debug.Log("Fired Jump");
        }
    }

}
