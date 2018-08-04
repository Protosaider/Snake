using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : SpawnerFromGrid
{
    public GameObject applePrefab;

    public void SpawnApple(Node node, MovementDirection direction)
    {
        SpawnObjectFromNode(applePrefab, node, rotationOffset: Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
    }

    public void SpawnSnakeBody(Node node, MovementDirection direction)
    {
        SpawnObjectFromNode(applePrefab, node);
    }
}
