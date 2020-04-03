using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject pauseButton;
    
    public GameObject activeColectables;
    public GameObject disabledColectables;

    public void GoLevelSelector()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Oshi");
    }

    public void PauseGame()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        activeColectables.SetActive(false);
        disabledColectables.SetActive(false);
    }

    public void ResumeGame()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
        pauseButton.SetActive(true);
        activeColectables.SetActive(true);
        disabledColectables.SetActive(true);
    }

    private void Start()
    {
        panel.SetActive(false);
    }


}
