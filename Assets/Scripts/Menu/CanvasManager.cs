using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject pauseButton;

    [SerializeField] private GameObject _teleportButton;
    [SerializeField] private GameObject _teleportMenu;

    [SerializeField] private LeanTweenType _teleportButtonIN;
    [SerializeField] private LeanTweenType _teleportButtonOUT;
    [SerializeField] private float teleportButtonAnimSpeed;

    [SerializeField] private LeanTweenType _teleportPanelIN;
    [SerializeField] private LeanTweenType _teleportPanelOUT;
    [SerializeField] private float teleportPanelAnimSpeed;



    public GameObject activeColectables;
    public GameObject disabledColectables;


    private void Start()
    {
        panel.SetActive(false);
        _teleportButton.transform.localScale = new Vector3(0, 0, 0);
        _teleportMenu.transform.localScale = new Vector3(0, 0, 0);
    }


    public void GoLevelSelector()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void ShowTelleportButton()
    {
        LeanTween.scale(_teleportButton, new Vector3(1, 1, 1), teleportButtonAnimSpeed).setEase(_teleportButtonIN);
    }

    public void HideTelleportButton()
    {
        LeanTween.scale(_teleportButton, new Vector3(0,0,0), teleportButtonAnimSpeed).setEase(_teleportButtonOUT);
    }

    public void ShowTelleportPanel()
    {
        LeanTween.scale(_teleportMenu, new Vector3(0.8f,0.8f, 1), teleportPanelAnimSpeed).setEase(_teleportPanelIN);
    }

    public void HideTelleportPanel()
    {
        LeanTween.scale(_teleportMenu, new Vector3(0, 0, 0), teleportPanelAnimSpeed).setEase(_teleportPanelOUT);
    }

}
