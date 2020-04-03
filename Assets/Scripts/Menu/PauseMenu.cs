using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;

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
        panel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        panel.SetActive(true);
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        panel.SetActive(false);
    }
}
