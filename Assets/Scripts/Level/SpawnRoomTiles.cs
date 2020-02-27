using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomTiles : MonoBehaviour
{
    //array of tiles that form the room structure
    public GameObject[] tilesArray;

    private void Start()
    {
        //random number generator between 0 and the maximum array size
        int rand = Random.Range(0, tilesArray.Length);
        //instatiate a diferent tile on the object position as a child
        //need to be a child of the room bcs when a room gets destroyed bcs of dead ends it needs to destroy the tiles too
        GameObject instance = (GameObject)Instantiate(tilesArray[rand],transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
