using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiscreteMeshGenerator
{
    public static Mesh GenerateGridMesh(MapSettings settings)
    {
        Mesh mesh = new Mesh();

        Vector2 bottomLeft;

        if (settings.isCentered)
        {
            bottomLeft = new Vector2(-settings.width, -settings.height) * settings.cellScale * 0.5f + new Vector2(settings.mapOffsetX, -settings.mapOffsetZ);
        }
        else
        {
            bottomLeft = new Vector2(settings.mapOffsetX, -settings.mapOffsetZ);
        }

        Vector3[] vertices = new Vector3[settings.width * settings.height * 4];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[settings.width * settings.height * 6]; //6 = 2 triangles * 3 vertices
        Color[] colors = new Color[vertices.Length];

        float vertexOffset = settings.cellScale;

        int vert = 0;
        int tri = 0;

        for (int y = 0; y < settings.height; y++)
        {
            for (int x = 0; x < settings.width; x++)
            {
                Vector3 cellOffset = new Vector3(x * settings.cellScale, 0.0f, y * settings.cellScale) + new Vector3(bottomLeft.x, 0.0f, bottomLeft.y);

                vertices[vert] = cellOffset;                                                        //bottom left
                vertices[vert + 1] = new Vector3(vertexOffset, 0.0f, 0.0f) + cellOffset;            //bottom right 
                vertices[vert + 2] = new Vector3(0.0f, 0.0f, vertexOffset) + cellOffset;            //top left
                vertices[vert + 3] = new Vector3(vertexOffset, 0.0f, vertexOffset) + cellOffset;    //top right

                uv[vert] = new Vector2((float)x / settings.width, (float)y / settings.height);
                uv[vert + 1] = new Vector2((float)(x + 1) / settings.width, (float)y / settings.height);
                uv[vert + 2] = new Vector2((float)x / settings.width, (float)(y + 1) / settings.height);
                uv[vert + 3] = new Vector2((float)(x + 1) / settings.width, (float)(y + 1) / settings.height);

                colors[vert] = colors[vert + 1] = colors[vert + 2] = colors[vert + 3] = settings.tilesColors[Random.Range(0, settings.tilesColors.Length)];

                triangles[tri + 2] = vert++;                        //_ _ 1 | _ _ _
                triangles[tri + 1] = triangles[tri + 3] = vert++;   //_ 2 1 | 2 _ _
                triangles[tri] = triangles[tri + 4] = vert++;       //3 2 1 | 2 3 _
                triangles[tri + 5] = vert++;                        //3 2 1 | 2 3 4
                tri += 6;
            }
        }

        mesh.name = "Discrete Mesh";
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();

        return mesh;
    }
}

