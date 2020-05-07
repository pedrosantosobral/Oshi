using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private List<RoomTypeDetection> roomlist =  new List<RoomTypeDetection>();
    private GameObject _player;

    public void AddRoom(RoomTypeDetection room)
    {
        roomlist.Add(room);
    }

    public void GetPlayer(GameObject player)
    {
        _player = GameObject.Find("Player(Clone)");
    }
}
