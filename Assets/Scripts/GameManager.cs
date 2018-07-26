using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private MapGenerator mapGenerator = new MapGenerator();
    private NodeGridGenerator nodeGenerator = new NodeGridGenerator();

    [SerializeField]
    private MapSettings settings;
    [SerializeField]
    private MeshSettings meshSettings;

    private NodeGrid mapGrid;

    private ObjectSpawner spawner;

    public GameObject body;

    // Use this for initialization
    private void Start ()
    {
        CreateMapRenderer();
        CreateNodeMap();

        CreateSpawner();

        Debug.Log("Default Vector3 = " + default(Vector3) + " default quaternion = " + default(Quaternion));
    }

    private void CreateSpawner()
    {
        GameObject go = new GameObject("Spawner");
        ObjectSpawner spawn = go.AddComponent<ObjectSpawner>();

        spawn.SetNodeGrid(mapGrid);
        spawn.SpawnObjectFromNode(body, 2, 2);
    }

    private void CreateMapRenderer()
    {
        mapGenerator.CreateMap(settings, meshSettings);
    }

    private void CreateNodeMap()
    {
        mapGrid = new NodeGrid(nodeGenerator.CreateNodeMap(settings), settings.width, settings.height);
        nodeGenerator.FillNodeMapWithWorldCoordinates(mapGrid, meshSettings);
    }

}
