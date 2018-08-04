using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapGenerator mapGenerator = new MapGenerator();
    public NodeGridGenerator nodeGenerator = new NodeGridGenerator();

    [SerializeField]
    private MapSettings settings;
    [SerializeField]
    private MeshSettings meshSettings;

    private NodeGrid mapGrid;
    public NodeGrid MapGrid
    {
        get { return mapGrid; }
    }

    public void Initialize()
    {
        CreateMapRenderer();
        CreateNodeMap();
    }

    private void CreateMapRenderer()
    {
        mapGenerator.CreateMap(settings, meshSettings).transform.SetParent(this.gameObject.transform);
    }

    private void CreateNodeMap()
    {
        mapGrid = new NodeGrid(nodeGenerator.CreateNodeMap(settings), settings.width, settings.height);
        nodeGenerator.FillNodeMapWithWorldCoordinates(mapGrid, meshSettings);
    }
}
