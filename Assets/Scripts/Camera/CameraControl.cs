using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float m_DampTime = 0.2f;                 // Approximate time for the camera to refocus.
    public float m_ScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
    public float m_MinSize = 6.5f;                  // The smallest orthographic size the camera can be.

    private Camera m_Camera;                        // Used for referencing the camera.
    private float m_ZoomSpeed;                      // Reference speed for the smooth damping of the orthographic size.
    private Vector3 m_Center;                       // The position the camera is moving towards.

    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
    }

    public void SetPositionAndSize(Vector3 bottomLeft, Vector3 upperRight)
    {
        FindAveragePosition(bottomLeft, upperRight);
        transform.position = m_Center;
        m_Camera.orthographicSize = FindRequiredSize(bottomLeft, upperRight);
    }

    //private void Zoom()
    //{
    //    // Find the required size based on the desired position and smoothly transition to that size.
    //    float requiredSize = FindRequiredSize();
    //    m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    //}

    private void FindAveragePosition(Vector3 bottomLeft, Vector3 upperRight)
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 2;

        averagePos += bottomLeft;
        averagePos += upperRight;

        if (numTargets > 0)
            averagePos /= numTargets;

        averagePos.y = transform.position.y;

        m_Center = averagePos;

        Debug.Log("Center " + m_Center);
    }

    private float FindRequiredSize(Vector3 bottomLeft, Vector3 upperRight)
    {
        // Find the position the camera rig is moving towards in its local space.
        Vector3 center = transform.InverseTransformPoint(m_Center);
        // Start the camera's size calculation at zero.
        float size = 0f;

        // Otherwise, find the position of the target in the camera's local space.
        Vector3 localPos = transform.InverseTransformPoint(bottomLeft);
        // Find the position of the target from the desired position of the camera's local space.
        Vector3 centerToTarget = localPos - center;
        // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
        size = Mathf.Max(size, Mathf.Abs(centerToTarget.z));
        // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
        size = Mathf.Max(size, Mathf.Abs(centerToTarget.x) / m_Camera.aspect);

        Debug.Log("Local 1 " + localPos);
        Debug.Log("CentToTarg 1 " + centerToTarget);

        localPos = transform.InverseTransformPoint(upperRight);
        centerToTarget = localPos - center;
        size = Mathf.Max(size, Mathf.Abs(centerToTarget.z));
        size = Mathf.Max(size, Mathf.Abs(centerToTarget.x) / m_Camera.aspect);

        Debug.Log("Local 2 " + localPos);
        Debug.Log("CentToTarg 2 " + centerToTarget);
        Debug.Log("Size " + size);

        // Add the edge buffer to the size.
        size += m_ScreenEdgeBuffer;
        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, m_MinSize);

        return size;
    }

}
