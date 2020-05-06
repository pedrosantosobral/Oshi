using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;
public class LevelGenerator : MonoBehaviour
{
    private int ammountOfColectibles = 3;
    private int ammountOfTeleports = 3;

    //LoadingScreen variables
    [SerializeField] private VoidEvent onPlayerSpawn;
    public Animator loadingScreen;

    //variables for the collectibles random positions generation
    public List<Pos> colectableRoomPositions;
    private List<int> _colectableRoomColumn;
    private List<int> _colectableRoomLine;

    //variables for the collectibles random positions generation
    public List<Pos> teleportRoomPositions;
    private List<int> _teleportRoomColumn;
    private List<int> _teleportRoomLine;

    //variables to check generation conclusion
    private bool    _calculationDone;
    private bool   _calculationDoneTeleport = false;
    public  bool    readyToPlayer;

    //dificulty variables
    public int numberOfHardRooms;
    public int numberOfMediumRooms;

    //reference of rooms
    private GameObject _toBeDestroyed;
    private Transform _roomTransformReference;
    
    [HideInInspector]
    public List<Transform> roomPositions;
    
    public LayerMask Room;
    //array of map starting positions that form the map structure
    public Transform[]  startPositions;

    //array of room that make part of the map
    public GameObject[] startingRooms;
    public GameObject[] endRooms;
    public GameObject[] rooms; //Index // 0 - 2 LR // 3 - 5 LRB // 6 - 8 LRT // 9 - 11 LRBT // 12 StartingRoom
    //variable to get the probability of each direction
    private int         _direction;
    private int         _downCounter;
    //variables to define the x and y space between the rooms
    public float        moveAmountLR;
    public float        moveAmountTD;

    //variables to define the max size of the entire map
    public float        minX;
    public float        maxX;
    public float        minY;
    
    //variable to stop the map generation
    public bool        stopGeneration;

    //variables to control the room spawning timer
    private float       _timeBetweenRooms;
    [SerializeField]
    private float       _startTimeBetweenRooms = 0.25f;
    private void FixedUpdate()
    {
        TimerBetweenRoomSpawns();
        CreateAIGraph();
        if(_calculationDone == false && stopGeneration == true)
        {
            //SET COLECTIBLES HERE AND MOVE READY TO PLAYER INSIDE
            if(ammountOfColectibles > 0  && ammountOfTeleports > 0)
            {
                GetColectableRoomsPositions(ammountOfColectibles);
                GetTeleportRoomsPositions(ammountOfTeleports);
            }
            else
            {
                _calculationDone = true;
                _calculationDoneTeleport = true;
            }
            
        }
    }

    private void Start()
    {
        colectableRoomPositions = new List<Pos>();
        _colectableRoomColumn = new List<int>();
        _colectableRoomLine = new List<int>();

        teleportRoomPositions = new List<Pos>();
        _teleportRoomColumn = new List<int>();
        _teleportRoomLine = new List<int>();

        _calculationDone    = false;
        _calculationDoneTeleport = false;
        stopGeneration      = false;
        readyToPlayer       = false;
        //random number generator between 0 and the maximum array size
        int rand = Random.Range(0, startPositions.Length);
        transform.position = startPositions[rand].position;
        
        //Spawn a starting room on a random position of the first row 
        _toBeDestroyed = Instantiate(startingRooms[0], transform.position, Quaternion.identity);
        _roomTransformReference = _toBeDestroyed.transform;
        roomPositions.Add(_roomTransformReference);


        //get a random number between 1 and 6
        _direction = Random.Range(1, 6);
    }

    //Level generation main function
    private void Move()
    {
        //Move right
        if(_direction == 1 || _direction == 2)
        {
            _downCounter = 0;
            //check if rooms reached the maximum right side
            if(transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmountLR, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                //instantiate a new room on a new founded position
                _toBeDestroyed = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                _roomTransformReference = _toBeDestroyed.transform;
                roomPositions.Add(_roomTransformReference);

                //get new random number
                _direction = Random.Range(1,6);

                //if statement to avoid the spawning of rooms over each other
                //if moved to the right it cant go back left and give a chance to go straight down
                //to dont take all the space in the same row
                if(_direction == 3)
                {
                    _direction = 2;
                }
                else if(_direction == 4)
                {
                    _direction = 5;
                }
            }
            //if yes, move down
            else
            {
                _direction = 5;
            }
        }
        //Move left
        else if (_direction == 3 || _direction == 4)
        {
            _downCounter = 0;
            //check if rooms reached the maximum left side
            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmountLR, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                //instantiate a new room on a new founded position
                _toBeDestroyed = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                _roomTransformReference = _toBeDestroyed.transform;
                roomPositions.Add(_roomTransformReference);

                //get new random number
                _direction = Random.Range(3, 6);
            }
            //if yes move down
            else
            {
                _direction = 5;
            }
        }
        //Move down
        else if (_direction == 5)
        {
            _downCounter++;
            //check if rooms reached the botoom of the lvl
            if(transform.position.y > minY)
            {
                if (_toBeDestroyed.GetComponent<RoomTypeDetection>().type != 1 && _toBeDestroyed.GetComponent<RoomTypeDetection>().type != 3 && _toBeDestroyed.GetComponent<RoomTypeDetection>().type != 8)
                {
                    int randomRoom = Random.Range(9, 11);
                    if (_downCounter >= 3)
                    {
                        
                        _toBeDestroyed.GetComponent<RoomTypeDetection>().DestroyRoom();
                        _toBeDestroyed = Instantiate(rooms[randomRoom], transform.position, Quaternion.identity);
                        _roomTransformReference = _toBeDestroyed.transform;
                        roomPositions.Add(_roomTransformReference);
                    }
                    else
                    {
                        //destroy the room
                        _toBeDestroyed.GetComponent<RoomTypeDetection>().DestroyRoom();

                        //get a random to get a room with a bottom opening
                        int randomBR = Random.Range(1, 4);

                        if (randomBR == 2)
                        {
                            randomBR = 1;
                        }
                        //use [3] while bug isnt fixed, than use randomBR
                        _toBeDestroyed = Instantiate(rooms[randomRoom], transform.position, Quaternion.identity);
                        
                        _roomTransformReference = _toBeDestroyed.transform;
                        roomPositions.Add(_roomTransformReference);
                    }
                    
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmountTD);
                transform.position = newPos;

                int rand = Random.Range(6, 11);
                //instantiate a new room on a new founded position
                _toBeDestroyed = Instantiate(rooms[rand], transform.position, Quaternion.identity);
               
                _roomTransformReference = _toBeDestroyed.transform;
                roomPositions.Add(_roomTransformReference);

                //get new random number
                _direction = Random.Range(1, 6);
                
            }
            //if yes stop the generation
            else
            {

                //Stop Lvl generation and spawn exit
                //destroy the room and spawn the exit
                roomPositions.Remove(_roomTransformReference);
                _toBeDestroyed.GetComponent<RoomTypeDetection>().DestroyRoom();
                _toBeDestroyed = Instantiate(endRooms[0], transform.position, Quaternion.identity);
                _roomTransformReference = _toBeDestroyed.transform;
                roomPositions.Add(_roomTransformReference);

                stopGeneration = true;

                for (int i = roomPositions.Count - 1; i > -1; i--)
                {
                    if (roomPositions[i] == null)
                        roomPositions.RemoveAt(i);
                }
            }
        }
    }

    private void TimerBetweenRoomSpawns()
    {
        //if the timer is 0 or the rooms dont reached the bottom yet
        if (_timeBetweenRooms <= 0 && stopGeneration == false)
        {
            //Spawn another room
            Move();
            //increase the timer to wait for other room
            _timeBetweenRooms = _startTimeBetweenRooms;
        }
        else
        {
            //decrease the timer
            _timeBetweenRooms -= Time.deltaTime;
        }
    }

    private void CreateAIGraph()
    {
        if(stopGeneration == true)
        {
            if (_calculationDone == false)
            {
                Invoke("ScanGraph", 0.1f);
            }
            
        }
    }

    private void ScanGraph()
    {
        AstarPath.active.Scan();
        Invoke("ReadyToPlayer", 0.1f);
    }

    private void ReadyToPlayer()
    {
        readyToPlayer = true;
        loadingScreen.SetBool("FadeScreen", readyToPlayer);
        onPlayerSpawn.Raise();
    }


    //recursive method to add numbers to the 2 lists
    private void GetColectableRoomsPositions(int ammountOfColectables)
    {
        int randLine;
        int randColumn;
        Pos randomPos;
        randomPos.line = 0;
        randomPos.column = 0;

            while (randomPos.line == 0)
            {
                randLine = Random.Range(1, 5);

                if (!_colectableRoomLine.Contains(randLine))
                {
                    randomPos.line = randLine;
                    _colectableRoomLine.Add(randLine);
                }

            }

            while (randomPos.column == 0)
            {
                randColumn = Random.Range(1, 5);

                if (!_colectableRoomColumn.Contains(randColumn))
                {
                    randomPos.column = randColumn;
                    _colectableRoomColumn.Add(randColumn);
                }
            }

            colectableRoomPositions.Add(randomPos);

            ammountOfColectables--;

            if (ammountOfColectables == 0)
            {
                DebugLists();
                _calculationDone = true;
            }
            else
            {
                GetColectableRoomsPositions(ammountOfColectables);
            }
          
    }

    private void GetTeleportRoomsPositions(int ammountOfTeleports)
    {
        int randLine;
        int randColumn;

        Pos randomPos;

        randomPos.line = 0;
        randomPos.column = 0;

        while (randomPos.line == 0)
        {
            randLine = Random.Range(1, 5);

            if (!_teleportRoomLine.Contains(randLine))
            {
                randomPos.line = randLine;
                _teleportRoomLine.Add(randLine);
            }

        }

        while (randomPos.column == 0)
        {
            randColumn = Random.Range(1, 5);

            if (!_teleportRoomColumn.Contains(randColumn))
            {
                randomPos.column = randColumn;
                _teleportRoomColumn.Add(randColumn);
            }
        }

        teleportRoomPositions.Add(randomPos);

        ammountOfTeleports--;

        if (ammountOfTeleports == 0)
        {
            DebugLists2();
            _calculationDoneTeleport = true;
        }
        else
        {
            GetTeleportRoomsPositions(ammountOfTeleports);
        }

    }

    private void DebugLists()
    {
        Debug.Log("Ha " + colectableRoomPositions.Count + " pos");
        foreach(Pos i in colectableRoomPositions)
        {
            Debug.Log("Pos " + "linha" + i.line + " coluna" + i.column);
        }


    }

    private void DebugLists2()
    {
        Debug.Log("Ha " + teleportRoomPositions.Count + " pos");
        foreach (Pos i in teleportRoomPositions)
        {
            Debug.Log("Pos " + "linha" + i.line + " coluna" + i.column);
        }


    }
}
