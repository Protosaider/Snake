using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapManager mapManager;
    public Player player;
    public CameraControl cameraControl;

    // Use this for initialization
    private void Start ()
    {
        cameraControl = FindObjectOfType<CameraControl>();
        mapManager = FindObjectOfType<MapManager>();

        mapManager.Initialize();

        cameraControl.SetPositionAndSize(mapManager.MapGrid.values[0, 0].pos, mapManager.MapGrid.values[mapManager.MapGrid.width - 1, mapManager.MapGrid.height - 1].pos);

        player.Initialize();
    }
}
