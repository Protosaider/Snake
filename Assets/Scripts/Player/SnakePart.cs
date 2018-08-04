using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : MonoBehaviour {

    public MovementDirection direction;
    public Transform trans;
    public Node occupiedNode;

    private float turnClockwise = 90.0f;
    private float turnCounterclockwise = -90.0f;

    private void Awake()
    {
        trans = GetComponent<Transform>();
    }

    public void Initialize(Node node, MovementDirection direction, bool hasRotationOnSpawn = true)
    {
        this.direction = direction;
        this.occupiedNode = node;

        if (hasRotationOnSpawn)
        {
            if (direction == MovementDirection.Right)
            {
                trans.Rotate(Vector3.up, turnClockwise);
            }
            else if (direction == MovementDirection.Down)
            {
                trans.Rotate(Vector3.up, 2 * turnClockwise);
            }
            else if (direction == MovementDirection.Left)
            {
                trans.Rotate(Vector3.up, 3 * turnClockwise);
            }
        }
    }

    public void MoveAndRotate(ref Node nextNode, ref MovementDirection nextDirection)
    {
        SwapNodes(ref nextNode);
        Move();
        Rotate(nextDirection);
        SwapDirection(ref nextDirection);
    }

    public void SwapNodes(ref Node nextNode)
    {
        //! Must be bug because classes - reference types
        Node swap = occupiedNode;
        occupiedNode = nextNode;
        nextNode = swap;
    }

    public void SwapDirection(ref MovementDirection dir)
    {
        MovementDirection swap = direction;
        direction = dir;
        dir = swap;
    }

    private void Move()
    {
        trans.position = occupiedNode.pos;
    }

    private void Rotate(MovementDirection nextDirection)
    {
        if (direction == MovementDirection.Up)
        {
            if (nextDirection == MovementDirection.Right)
            {
                trans.Rotate(Vector3.up, turnClockwise);
            }
            else if (nextDirection == MovementDirection.Left)
            {
                trans.Rotate(Vector3.up, turnCounterclockwise);
            }
        }
        else if (direction == MovementDirection.Right)
        {
            if (nextDirection == MovementDirection.Down)
            {
                trans.Rotate(Vector3.up, turnClockwise);
            }
            else if (nextDirection == MovementDirection.Up)
            {
                trans.Rotate(Vector3.up, turnCounterclockwise);
            }
        }
        else if (direction == MovementDirection.Down)
        {
            if (nextDirection == MovementDirection.Left)
            {
                trans.Rotate(Vector3.up, turnClockwise);
            }
            else if (nextDirection == MovementDirection.Right)
            {
                trans.Rotate(Vector3.up, turnCounterclockwise);
            }
        }
        else if (direction == MovementDirection.Left)
        {
            if (nextDirection == MovementDirection.Up)
            {
                trans.Rotate(Vector3.up, turnClockwise);
            }
            else if (nextDirection == MovementDirection.Down)
            {
                trans.Rotate(Vector3.up, turnCounterclockwise);
            }
        }
    }
}
