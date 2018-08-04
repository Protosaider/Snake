using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : SpawnerFromGrid
{
    public GameObject headPrefab;
    public GameObject bodyPrefab;

    public void SpawnSnakeHead(Node node, MovementDirection direction)
    {
        SpawnObjectFromNode(headPrefab, node).GetComponent<SnakePart>().Initialize(node, direction, false);
    }

    public void SpawnSnakeBody(Node node, MovementDirection direction)
    {
        SpawnObjectFromNode(bodyPrefab, node).GetComponent<SnakePart>().Initialize(node, direction);
    }
}
