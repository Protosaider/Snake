using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid
{
    public int width, height;
    public Node[,] values;

    public NodeGrid(Node[,] nodeMap, int width, int height)
    {
        this.width = width;
        this.height = height;
        values = nodeMap;
    }

    public Node GetNode(int x, int z)
    {
        if (IsOutside(x, z))
        {
            //return Node.Null;
            return null;
        }

        return values[x, z];
    }

    public Node GetNode(int x, int z, out bool isOutside)
    {
        isOutside = IsOutside(x, z);

        if (isOutside)
        {
            return null;
        }
        else
        {
            return values[x, z];
        }
    }

    public bool IsOutside(int x, int z)
    {
        return (x < 0) || (x >= width) || (z < 0) || (z >= height);
    }
    
    public Vector3 GetWorldPos(int x, int z, out bool isOutside)
    {
        isOutside = IsOutside(x, z);

        if (isOutside)
        {
            return default(Vector3);
        }
        else
        {
            return values[x, z].pos;
        }
    }

    public Vector3 GetWorldPos(MovementDirection direction, out bool isOutside)
    {
        Vector3Int offset = offsets[direction];
        isOutside = IsOutside(offset.x, offset.z);

        if (isOutside)
        {
            return default(Vector3);
        }
        else
        {
            return values[offset.x, offset.z].pos;
        }
    }

    public Vector3 GetWorldPos(MovementDirection direction)
    {
        return values[offsets[direction].x, offsets[direction].z].pos;
    }

    //public static Dictionary<MovementDirection, Vector3> offsets = new Dictionary<MovementDirection, Vector3>(System.Enum.GetValues(typeof(MovementDirection)).Length)
    public static Dictionary<MovementDirection, Vector3Int> offsets = new Dictionary<MovementDirection, Vector3Int>()
    {
        { MovementDirection.Up, new Vector3Int(0, 0, 1) },
        { MovementDirection.Right, new Vector3Int(1, 0, 0) },
        { MovementDirection.Down, new Vector3Int(0, 0, -1) },
        { MovementDirection.Left, new Vector3Int(-1, 0, 0) },
    };
}
