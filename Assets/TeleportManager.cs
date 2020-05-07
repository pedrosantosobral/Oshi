using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private List<RoomTypeDetection> roomlist =  new List<RoomTypeDetection>();
    private GameObject _player;

    [SerializeField] private VoidEvent hidePanel;

    public void AddRoom(RoomTypeDetection room)
    {
        roomlist.Add(room);
    }

    public void GetPlayer()
    {
        _player = GameObject.Find("Player(Clone)");
    }

    public void TeleportPlayer(Pos pos)
    {
        foreach(RoomTypeDetection room in roomlist)
        {
            if(room.roomPos.column == pos.column && room.roomPos.line == pos.line)
            {
                //teleport player to teleport position;
                //LeanTween.moveX(_player, room.teleportReference.gameObject.transform.position.x, 0f);
                //LeanTween.moveY(_player, room.teleportReference.gameObject.transform.position.y, 0f);
                _player.transform.position = room.teleportReference.gameObject.transform.position;
                hidePanel.Raise();
                break;
            }
        }
    }

}
