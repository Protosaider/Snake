using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private MapGenerator mapGenerator = new MapGenerator();
    private NodeGridGenerator nodeGenerator = new NodeGridGenerator();

    private CameraControl cameraControl;

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
        cameraControl = FindObjectOfType<CameraControl>();

        CreateMapRenderer();
        CreateNodeMap();

        CreateSpawner();

        Debug.Log(mapGrid.values[0, 0].pos);

        Debug.Log(mapGrid.values[mapGrid.width - 1, mapGrid.height - 1].pos);

        cameraControl.SetPositionAndSize(mapGrid.values[0, 0].pos, mapGrid.values[mapGrid.width - 1, mapGrid.height - 1].pos);

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
