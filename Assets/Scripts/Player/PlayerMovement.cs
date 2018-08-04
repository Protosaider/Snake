using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int snakeLength = 2;

    public NodeGrid nodeGrid;

    public SnakePart[] snake;

    public MovementDirection currentDirection = MovementDirection.None;
    public bool isMoving;
    public bool hasChangedDirection;

    private Node nextNode;

    //? is it faster?
    //? is it better to use just deltaTime?
    public WaitForSeconds moveTime = new WaitForSeconds(0.3f);

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            ChangeDirection();
        }
    }

    public void Initialize()
    {
        nodeGrid = FindObjectOfType<MapManager>().MapGrid;
        StartCoroutine(StartMovement());
    }

    public IEnumerator StartMovement()
    {
        while (currentDirection == MovementDirection.None)
        {
            Debug.Log("In start movement");

            if (Input.GetKeyDown(KeyCode.D))
            {
                currentDirection = MovementDirection.Right;
                hasChangedDirection = true;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                currentDirection = MovementDirection.Left;
                hasChangedDirection = true;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                currentDirection = MovementDirection.Up;
                hasChangedDirection = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currentDirection = MovementDirection.Down;
                hasChangedDirection = true;
            }

            yield return null;
        }

        isMoving = true;
        StartCoroutine(Move());
    }

    public bool IsPossibleToMove()
    {
        bool isOutside;

        nextNode = nodeGrid.GetNode(snake[0].occupiedNode.x + NodeGrid.offsets[currentDirection].x, snake[0].occupiedNode.z + NodeGrid.offsets[currentDirection].z, out isOutside);

        if (isOutside)
        {
            Debug.Log("Is outside");
            return false;
        }

        if (IsEatingYourself())
        {
            Debug.Log("Eat yourself");
            return false;
        }

        return true;
    }

    public void MoveSnake()
    {
        MovementDirection swapDir = currentDirection;

        for (int i = 0; i < snakeLength; i++)
        {
            snake[i].MoveAndRotate(ref nextNode, ref swapDir);
        }
    }

    public bool IsEatingYourself()
    {
        for (int i = 1; i < snakeLength; i++)
        {
            if (snake[i].occupiedNode == nextNode)
            {
                return true;
            }
        }
        return false;
    }

    public IEnumerator Move()
    {
        while (isMoving)
        {
            if (currentDirection == MovementDirection.Up)
            {
                if (IsPossibleToMove())
                {
                    MoveSnake();
                }
                else
                {
                    isMoving = false;
                    yield return null;
                }

                Debug.Log("move up");
                hasChangedDirection = false;
                yield return moveTime;
            }
            else if (currentDirection == MovementDirection.Right)
            {
                if (IsPossibleToMove())
                {
                    MoveSnake();
                }
                else
                {
                    isMoving = false;
                    yield return null;
                }

                Debug.Log("move right");
                hasChangedDirection = false;
                yield return moveTime;
            }
            else if (currentDirection == MovementDirection.Down)
            {
                if (IsPossibleToMove())
                {
                    MoveSnake();
                }
                else
                {
                    isMoving = false;
                    yield return null;
                }

                Debug.Log("move down");
                hasChangedDirection = false;
                yield return moveTime;
            }
            else if (currentDirection == MovementDirection.Left)
            {
                if (IsPossibleToMove())
                {
                    MoveSnake();
                }
                else
                {
                    isMoving = false;
                    yield return null;
                }

                Debug.Log("move left");
                hasChangedDirection = false;
                yield return moveTime;
            }
        }
    }

    public void ChangeDirection()
    {
        if (hasChangedDirection)
        {
            return;
        }

        if (currentDirection == MovementDirection.Up)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                currentDirection = MovementDirection.Right;
                hasChangedDirection = true;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                currentDirection = MovementDirection.Left;
                hasChangedDirection = true;
            }
        }
        else if (currentDirection == MovementDirection.Right)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentDirection = MovementDirection.Up;
                hasChangedDirection = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currentDirection = MovementDirection.Down;
                hasChangedDirection = true;
            }
        }
        else if (currentDirection == MovementDirection.Down)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                currentDirection = MovementDirection.Right;
                hasChangedDirection = true;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                currentDirection = MovementDirection.Left;
                hasChangedDirection = true;
            }
        }
        else if (currentDirection == MovementDirection.Left)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentDirection = MovementDirection.Up;
                hasChangedDirection = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currentDirection = MovementDirection.Down;
                hasChangedDirection = true;
            }
        }

        Debug.Log("Current direction " + currentDirection);
    }

}
