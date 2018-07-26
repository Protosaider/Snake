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

    public bool IsOutside(int x, int z)
    {
        return (x < 0) || (x >= width) || (z < 0) || (z >= height);
    }
}
