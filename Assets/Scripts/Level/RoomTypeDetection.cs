using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTypeDetection : MonoBehaviour
{
    private LevelGenerator _lvlGeneratorReference;

    public GameObject colectableToSpawn;
    public GameObject lightReference;
    public GameObject teleportReference;

    public int  type;
    public int  dificulty; //0 - EASY //1 - MEDIUM //2 - HARD
    public Pos roomPos;

    public bool temp = false;
    public bool noteleport = false;
    bool doneOnce = false;

    private void Start()
    {
        //_colectableToSpawn = GameObject.Find
        _lvlGeneratorReference = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();

        roomPos.column = GetRoomColumn();
        roomPos.line = GetRoomLine();
    }

    public void DestroyRoom()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// Methood to get room row, 1 top ,4 bottom, 0 undefined
    /// </summary>
    private int GetRoomLine()
    {
        if(Mathf.Approximately(gameObject.transform.position.y,0f))
        {
            return 1;
        }
        else if (Mathf.Approximately(gameObject.transform.position.y, -10f))
        {
            return 2;
        }
        else if (Mathf.Approximately(gameObject.transform.position.y, -20f))
        {
            return 3;
        }
        else if (Mathf.Approximately(gameObject.transform.position.y, -30f))
        {
            return 4;
        }
        else
        {
            return 0;
        }

    }

    private int GetRoomColumn()
    {
        if (Mathf.Approximately(gameObject.transform.position.x, 0f))
        {
            return 1;
        }
        else if (Mathf.Approximately(gameObject.transform.position.x, 20f))
        {
            return 2;
        }
        else if (Mathf.Approximately(gameObject.transform.position.x, 40f))
        {
            return 3;
        }
        else if (Mathf.Approximately(gameObject.transform.position.x, 60f))
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }

    public void SetObjects()
    {
        if(!_lvlGeneratorReference.colectableRoomPositions.Contains(roomPos))
        {

            if (colectableToSpawn != null)
            {
                colectableToSpawn.SetActive(false);
            }
                

            if (lightReference != null)
            {
                lightReference.SetActive(true);
            }
                
        }

        if (_lvlGeneratorReference.colectableRoomPositions.Contains(roomPos))
        {
            temp = true;

            if (lightReference != null)
            {
                lightReference.SetActive(false);
            }
                

            if (colectableToSpawn != null)
            {
                colectableToSpawn.SetActive(true);
            }
                
        }

        if (!_lvlGeneratorReference.teleportRoomPositions.Contains(roomPos))
        {
            noteleport = true;

            if (teleportReference != null)
            {
                teleportReference.SetActive(false);
            }

        }
    }
}
