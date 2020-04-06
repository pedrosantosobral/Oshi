﻿using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;
public class LevelGenerator_Tutorial : MonoBehaviour
{

    //LoadingScreen variables
    [SerializeField] private VoidEvent onPlayerSpawn;
    public Animator loadingScreen;

    private bool    _calculationDone;
    public  bool    readyToPlayer;
    public float    loadingScreenTimer;

    public LayerMask Room;

    //variable to stop the map generation
    public bool        stopGeneration;

    //variables to control the room spawning timer
    private float       _timeBetweenRooms;
    [SerializeField]
    private float       _startTimeBetweenRooms = 0.25f;
    private void Start()
    {
        stopGeneration = false;
        _calculationDone = false;
        
        
    }

    private void Update()
    {
        if(loadingScreenTimer > -0.1)
        {
            loadingScreenTimer -= Time.deltaTime;
        }
        
        if (loadingScreenTimer < 0)
        {
            StopLoadingScreen();

        }
    }

    private void CreateAIGraph()
    {

        bool doOnce = true;

        if(stopGeneration == true)
        {
            if (_calculationDone == false)
            {
                if(doOnce == true)
                {
                    Invoke("ScanGraph", 0.1f);
                    doOnce = false;
                }
                
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

    private void StopLoadingScreen()
    {
        stopGeneration = true;
        CreateAIGraph();
    }

}
