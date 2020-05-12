using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerFolow : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private GameObject follow;


    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    public void GetPlayer()
    {
       follow = GameObject.Find("Player(Clone)");
       vcam.Follow = follow.transform;
    }
}
