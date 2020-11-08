using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [SerializeField] private Grid grid;
    [HideInInspector] public Vector2Int curruntcollectablePos;

    public GameObject collectablePrefab;

    private GameObject curruntCollectable;

    
    void Start()
    {
        //Spawn a collectable on start of game
        SpawnCollectable();
    }


    //Spawns a collectable in a random location in the grid
    public void SpawnCollectable()
    {
        Vector2Int randomGridPos = new Vector2Int(Random.Range(0, grid.width), Random.Range(0, grid.height));

        curruntCollectable = Instantiate(collectablePrefab, grid.GridToScreenPos(randomGridPos.x, randomGridPos.y), Quaternion.identity);

        curruntcollectablePos = randomGridPos;
    }

    //Destroys the current collectable
    public void DestroyFood()
    {
        Destroy(curruntCollectable);
    }
}
