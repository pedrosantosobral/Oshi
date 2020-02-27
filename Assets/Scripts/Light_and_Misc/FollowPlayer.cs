using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject _playerReference;
    public  GameObject thisGameObject;

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        if(_playerReference != null)
        {
            thisGameObject.transform.position = _playerReference.transform.position;
            thisGameObject.transform.position += new Vector3(0, 0, -8f);
        }
        
    }

    private void FindPlayer()
    {
        if(_playerReference == null)
        {
            _playerReference = GameObject.FindGameObjectWithTag("Player");
        }
        
    }
}
