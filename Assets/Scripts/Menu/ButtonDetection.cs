using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDetection : MonoBehaviour
{
    [SerializeField] private AudioClip teleportSound;


    private LevelGenerator _lvlGeneratorReference;
    private GameObject myRoom;
    private TeleportManager _teleportManagerReference;

    public GameObject buttonToActivate;

    public int line;
    public int column;

    public Pos buttonPos;
    private void Start()
    {
        buttonPos.line = line;
        buttonPos.column = column;
        _lvlGeneratorReference = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        _teleportManagerReference = GameObject.Find("TeleportManager").GetComponent<TeleportManager>();
    }

    public void CheckButtonActivity()
    {
        if (!_lvlGeneratorReference.teleportRoomPositions.Contains(buttonPos))
        {
            buttonToActivate.SetActive(false);
        }
    }

    public void PressButton()
    {
        AudioManager.Instance.PlaySFX(teleportSound,0.5f);
        _teleportManagerReference.TeleportPlayer(buttonPos);
    }

}
