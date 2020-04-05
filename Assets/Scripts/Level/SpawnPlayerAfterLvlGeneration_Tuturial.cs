using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerAfterLvlGeneration_Tutorial : MonoBehaviour
{

    public  GameObject player;
    private bool       _doOnce;
    private LevelGenerator_Tutorial _lvlGenReference;
    // Start is called before the first frame update
    void Start()
    {
        _doOnce = false;
        _lvlGenReference = GameObject.Find("LevelGenerator_Tutorial").GetComponent<LevelGenerator_Tutorial>();  
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
