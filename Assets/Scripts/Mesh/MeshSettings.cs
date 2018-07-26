using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mesh Settings")]
public class MeshSettings : ScriptableObject
{
    public float cellScale = 10.0f;

    public bool isCentered = true;
    public float mapOffsetX, mapOffsetZ;
}
