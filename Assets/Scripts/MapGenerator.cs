using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    public GameObject CreateMap(MapSettings settings, out Renderer renderer)
    {
        GameObject map = new GameObject("Tile Map");

        //GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        //GameObject.Destroy(gameObject);

        MeshFilter filter = map.AddComponent<MeshFilter>();

        //filter.sharedMesh = mesh;
        filter.sharedMesh = DiscreteMeshGenerator.GenerateGridMesh(settings);

        renderer = map.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = new Material(Shader.Find("Unlit/Texture"))
        {
            mainTexture = GenerateMapTexture(settings)
        };
        //renderer.transform.localScale = new Vector3(settings.width, 1.0f, settings.height) / settings.cellScale;

        return map;
    }

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

    private Texture2D GenerateMapTexture(MapSettings settings)
    {
        Texture2D texture = new Texture2D(settings.width, settings.height);
        Color[] colors = new Color[settings.width * settings.height];

        for (int z = 0; z < settings.height; z++)
        {
            for (int x = 0; x < settings.width; x++)
            {
                colors[x + z * settings.width] = settings.tilesColors[Random.Range(0, settings.tilesColors.Length)];
            }
        }

        texture.SetPixels(colors);
        texture.filterMode = FilterMode.Point;
        texture.Apply();

        return texture;
    }
}
