﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Settings")]
public class PlayerSettings : ScriptableObject
{
    public int spawnX = 3, spawnZ = 3;
    public Color color;

    public float minPlayerScale = 0.7f;
    public float maxPlayerScale = 1.0f;
}
