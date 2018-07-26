using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGridGenerator
{
    public Node[,] CreateNodeMap(MapSettings settings)
    {
        //Node[] nodeMap = new Node[settings.width * settings.height];
        Node[,] nodeMap = new Node[settings.width, settings.height];

        for (int z = 0; z < settings.height; z++)
        {
            for (int x = 0; x < settings.width; x++)
            {
                //nodeMap[x + z * settings.width] = new Node() { x = x, z = z, pos = Vector3.zero };
                nodeMap[x, z] = new Node(x, z, Vector3.zero);
            }
        }

        return nodeMap;
    }

    public void FillNodeMapWithWorldCoordinates(NodeGrid nodeGrid, MeshSettings meshSettings)
    {
        Vector2 bottomLeft;

        if (meshSettings.isCentered)
        {
            bottomLeft = new Vector2(-nodeGrid.width, -nodeGrid.height) * meshSettings.cellScale * 0.5f + new Vector2(meshSettings.mapOffsetX, -meshSettings.mapOffsetZ);
        }
        else
        {
            bottomLeft = new Vector2(meshSettings.mapOffsetX, -meshSettings.mapOffsetZ);
        }

        float vertexOffset = meshSettings.cellScale;

        Vector3 halfCell = new Vector3(vertexOffset, 0.0f, vertexOffset) * 0.5f;

        for (int y = 0; y < nodeGrid.height; y++)
        {
            for (int x = 0; x < nodeGrid.width; x++)
            {
                Vector3 cellOffset = new Vector3(x * meshSettings.cellScale, 0.0f, y * meshSettings.cellScale) + new Vector3(bottomLeft.x, 0.0f, bottomLeft.y);

                //! Cell center
                nodeGrid.values[x, y].pos = halfCell + cellOffset;
            }
        }
    }
}
