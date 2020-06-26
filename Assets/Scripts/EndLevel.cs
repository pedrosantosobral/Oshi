using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndLevel : MonoBehaviour
{

    [SerializeField] private AudioClip winMusic;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayMusicWithCrossFade(winMusic);
            SceneManager.LoadScene("WinScreen");
        }
      
    }
}
