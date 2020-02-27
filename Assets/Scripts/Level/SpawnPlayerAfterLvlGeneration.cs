using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerAfterLvlGeneration : MonoBehaviour
{

    public  GameObject player;
    private bool       _doOnce;
    private LevelGenerator _lvlGenReference;
    // Start is called before the first frame update
    void Start()
    {
        _doOnce = false;
        _lvlGenReference = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();  
    }

    private void Update()
    {
        if (_lvlGenReference.readyToPlayer == true && _doOnce == false)
        {
            Instantiate(player, gameObject.transform.position, Quaternion.identity);
            _doOnce = true;
        }
    }




}
