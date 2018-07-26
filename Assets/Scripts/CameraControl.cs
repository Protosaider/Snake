using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private Camera m_Camera;

    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
    }


    public void SetStartPositionAndSize()
    {
        //FindAveragePosition();

        //transform.position = m_DesiredPosition;

        //m_Camera.orthographicSize = FindRequiredSize();
    }
}
