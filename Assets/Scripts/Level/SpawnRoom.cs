using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LevelGenerator levelGeneratorReference;
    private bool checkCounter = false;
    private bool checkdone = false;
    private bool checkConclusion = false;

    private void FixedUpdate()
    {
        checkdone = levelGeneratorReference.stopGeneration;

        if(checkdone == true)
        {
            foreach (Transform i in levelGeneratorReference.roomPositions)
            {
                //compares transforms in array with actual transform
                if (Mathf.Approximately(i.position.x, transform.position.x) && Mathf.Approximately(i.position.y, transform.position.y))
                {
                    checkCounter = true;
                } 
            }
            checkConclusion = true;
        }

        if (checkConclusion == true && checkdone == true)
        {
            SpawnRooms();
        }
    }
    private void SpawnRooms()
    {
        //if this transform is not on the list spawn a random room
        if (checkCounter == false)
        {

            int rand = Random.Range(0,levelGeneratorReference.All_Rooms.Count);
            Instantiate(levelGeneratorReference.All_Rooms[rand], transform.position, Quaternion.identity);
            levelGeneratorReference.All_Rooms.RemoveAt(rand);
            Destroy(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
}
