using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDetection : MonoBehaviour
{
    [SerializeField] private AudioClip teleportSound;
    [SerializeField] private GameObject youAreHereMark;

    private LevelGenerator _lvlGeneratorReference;
    private PlayerInteractions _playerReference;
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

   
    public void CheckForPlayer()
    {
        if (_playerReference == null)
        {
            _playerReference = GameObject.Find("Player(Clone)").GetComponent<PlayerInteractions>();
        }

        if (line == _playerReference.playerLine && column == _playerReference.playerCol)
        {
            youAreHereMark.SetActive(true);
        }
        else
        {
            youAreHereMark.SetActive(false);
        }

        CheckButtonActivity();
    }

    public void CheckButtonActivity()
    {
        if (!_lvlGeneratorReference.teleportRoomPositions.Contains(buttonPos))
        {
            buttonToActivate.SetActive(false);
        }
        else
        {
            buttonToActivate.SetActive(true);
        }
    }

    public void PressButton()
    {
        AudioManager.Instance.PlaySFX(teleportSound,0.5f);
        _teleportManagerReference.TeleportPlayer(buttonPos);
    }

}
