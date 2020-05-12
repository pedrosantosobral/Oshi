using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private List<RoomTypeDetection> roomlist = new List<RoomTypeDetection>();
    [SerializeField] private GameObject camera;
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
                //LeanTween.moveX(camera, room.teleportReference.gameObject.transform.position.x, 1f);
                //LeanTween.moveY(camera, room.teleportReference.gameObject.transform.position.y, f);
                _player.transform.position = room.teleportReference.gameObject.transform.position;
                hidePanel.Raise();
                break;
            }
        }
    }

}
