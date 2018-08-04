using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public int x;
    public int z;
    public Vector3 pos;

    //public static Node Null = new Node(int.MinValue, int.MinValue, Vector3.negativeInfinity);

    public Node(int x, int z, Vector3 worldPos)
    {
        this.x = x;
        this.z = z;
        pos = worldPos;
    }

    public Node(Node node)
    {
        x = node.x;
        z = node.z;
        pos = node.pos;
    }

}
