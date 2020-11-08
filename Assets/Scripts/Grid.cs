using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public int width = 100;
    public int height = 100;
    private int[,] gridArray;

    //create a grid used for storing positions at the start of the game
    private void Start()
    {
        gridArray = new int[width, height];
    }
  
    //Returns a position on screen based on a given grid co-ordinate
    public Vector2 GridToScreenPos(int x, int y)
    {
        float posX = (Screen.width / width) * x;
        float posY = (Screen.height / height) * y;

        return Camera.main.ScreenToWorldPoint(new Vector2(posX, posY));
    }

    //Returns a random screen position used for instantiating a new collectable
    public Vector2 GetRandomScreenPosition()
    {
        return GridToScreenPos(Random.Range(0, width), Random.Range(0, height));
    }

    //Returns the grid position in the centre of the screen
    public Vector2 CentreGridPosition()
    {
        return GridToScreenPos(width/2, height / 2);
    }
}
