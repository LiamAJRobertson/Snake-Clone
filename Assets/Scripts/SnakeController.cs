using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject snakeBodyPrefab;
    [SerializeField] private ItemSpawner spawner;
    [SerializeField] private GameController gC;

    private Rigidbody2D rb;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private List<Vector2Int> snakeBodyPartPos = new List<Vector2Int>();
    private List<Vector2Int> snakeMovePosList = new List<Vector2Int>();
    private List<Transform> snakeBodyTransfromList = new List<Transform>();
    private Vector2Int gridMoveDirection;
    private int snakeBodySize;
    

    private void Awake()
    {
        //Initialize values when the program starts
        gridPosition = new Vector2Int(10, 5);
        gridMoveTimerMax = 0.25f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);
    }


    // Update is called once per frame
    void Update()
    {
        //Handle user input, using W,A,S,D for movement
        UserInput();

        //Update game timer 
        gridMoveTimer += Time.deltaTime;

        if(gridMoveTimer >= gridMoveTimerMax)
        {
            //Reset timer if the max value is met
            gridMoveTimer -= gridMoveTimerMax;

            GameOverOnCollision();
            UpdateSnakeBodyPosition();
            SwitchDirectionOffScreen();

            //Update the snake head's grid position
            gridPosition += gridMoveDirection;

        }

        //Update the snake Heads Position using the current grid co-ordinates
        transform.position = grid.GridToScreenPos(gridPosition.x, gridPosition.y);

        //If snake head is on the same position as food, then eat food
        if(gridPosition == spawner.curruntcollectablePos)
        {
            EatFood();
        }
    }

    //Create a new snake body when the snaked collects some food and store the transform and position in their corresponding lists
    void CreateSnakeBody()
    {
        GameObject snakeBody = Instantiate(snakeBodyPrefab, grid.GridToScreenPos(gridPosition.x, gridPosition.y), Quaternion.identity);
        snakeBodyTransfromList.Add(snakeBody.transform);
        snakeBodyPartPos.Add(new Vector2Int(-1, -1));
    }

    //Change the snake's direction based on the user's input
    void UserInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gridMoveDirection.x = 0;
            gridMoveDirection.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            gridMoveDirection.x = -1;
            gridMoveDirection.y = 0;
        }
        if (Input.GetKey(KeyCode.S))
        {
            gridMoveDirection.x = 0;
            gridMoveDirection.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            gridMoveDirection.x = 1;
            gridMoveDirection.y = 0;
        }
    }

    //Change the snake heads position if you go off the side of the screen bounds
    void SwitchDirectionOffScreen()
    {
        if (gridPosition.x > grid.width)
        {
            gridPosition.x = 0;
        }
        if (gridPosition.x < 0)
        {
            gridPosition.x = grid.width;
        }
        if (gridPosition.y > grid.height)
        {
            gridPosition.y = 0;
        }
        if (gridPosition.y < 0)
        {
            gridPosition.y = grid.height;
        }
    }

    //Loop through all the current body part's positions and end the game if the current snake head is on the same grid square as a body part
    void GameOverOnCollision()
    {
        foreach (Vector2Int bodyPartPos in snakeBodyPartPos)
        {
            if (gridPosition == bodyPartPos)
            {
                //Game Over
                gC.GameOver();
            }
        }
    }

    void UpdateSnakeBodyPosition()
    {
        //Store initial grid Position of snake head
        snakeMovePosList.Insert(0, gridPosition);

        //Check to ensure the number of snake moves stored isn't greater than current snake size
        if (snakeMovePosList.Count >= snakeBodySize + 1)
        {
            snakeMovePosList.RemoveAt(snakeMovePosList.Count - 1);
        }
        
        //Loop through all the snake bodies and update their position based on the snake heads previous position
        for (int i = 0; i < snakeBodyTransfromList.Count; i++)
        {
            Vector2 snakeBodyPos = new Vector2(snakeMovePosList[i].x, snakeMovePosList[i].y);
            snakeBodyTransfromList[i].position = grid.GridToScreenPos((int)snakeBodyPos.x, (int)snakeBodyPos.y);
            snakeBodyPartPos[i] = snakeMovePosList[i];
        }
    }

    //Destroy food, increase snakeBody count, spawn new collectable and the update score in GameController
    void EatFood()
    {
        spawner.DestroyFood();
        snakeBodySize++;
        CreateSnakeBody();
        spawner.SpawnCollectable();
        gC.AddToScore();
    }

}
