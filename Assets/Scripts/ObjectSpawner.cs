using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    NodeGrid grid;

    public void SetNodeGrid(NodeGrid grid)
    {
        this.grid = grid;
    }

    public void SpawnObjectFromNode(GameObject obj, int x, int z, Vector3 positionOffset = default(Vector3), Quaternion rotationOffset = default(Quaternion))
    {
        if (grid.GetNode(x, z) == null)
        {
            Debug.LogError("Node coordinates are outside of bounds");
            return;
        }

        Instantiate(obj, grid.values[x, z].pos + positionOffset, Quaternion.identity * rotationOffset);
    }

    public void SpawnObject(GameObject obj, Vector3 position, Quaternion rotation)
    {
        Instantiate(obj, position, rotation);
    }
}
