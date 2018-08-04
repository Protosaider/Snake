using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFromGrid : MonoBehaviour
{
    NodeGrid grid;

    public void SetNodeGrid(NodeGrid grid)
    {
        this.grid = grid;
    }

    public GameObject SpawnObjectFromGrid(GameObject obj, int x, int z, Vector3 positionOffset = default(Vector3), Quaternion rotationOffset = default(Quaternion))
    {
        if (grid.GetNode(x, z) == null)
        {
            Debug.LogError("Node coordinates are outside of bounds");
            return null;
        }

        return Instantiate(obj, grid.values[x, z].pos + positionOffset, Quaternion.identity * rotationOffset);
    }

    public GameObject SpawnObjectFromNode(GameObject obj, Node node, Vector3 positionOffset = default(Vector3), Quaternion rotationOffset = default(Quaternion))
    {
        if (grid.IsOutside(node.x, node.z))
        {
            Debug.LogError("Node coordinates are outside of bounds");
            return null;
        }

        return Instantiate(obj, node.pos + positionOffset, Quaternion.identity * rotationOffset);
    }

    public GameObject SpawnObject(GameObject obj, Vector3 position, Quaternion rotation)
    {
        return Instantiate(obj, position, rotation);
    }
}
