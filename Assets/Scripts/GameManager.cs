using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private MapGenerator mapGenerator = new MapGenerator();

    [SerializeField]
    private MapSettings settings;

    private GameObject map;
    private Renderer mapRenderer;

    private NodeGrid mapGrid;

    // Use this for initialization
    private void Start ()
    {
        map = mapGenerator.CreateMap(settings, out mapRenderer);
        mapGrid = new NodeGrid(mapGenerator.CreateNodeMap(settings), settings.width, settings.height);
    }
	
	// Update is called once per frame
	private void Update ()
    {
		
	}
}
