using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map Settings")]
public class MapSettings : ScriptableObject
{
    public int width = 17;
    public int height = 15;

    public Color[] tilesColors;
}
