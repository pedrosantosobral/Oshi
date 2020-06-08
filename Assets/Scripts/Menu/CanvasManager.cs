using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    

    public GameObject panel;
    public GameObject pauseButton;

    [SerializeField] private CanvasGroup blackPannel;
    [SerializeField] private GameObject _teleportButton;
    [SerializeField] private GameObject _teleportMenu;

    [SerializeField] private LeanTweenType _teleportButtonIN;
    [SerializeField] private LeanTweenType _teleportButtonOUT;
    [SerializeField] private float teleportButtonAnimSpeed;

    [SerializeField] private LeanTweenType _teleportPanelIN;
    [SerializeField] private LeanTweenType _teleportPanelOUT;
    [SerializeField] private float teleportPanelAnimSpeed;

    [SerializeField] private LeanTweenType _blackPanelFadeIN;
    [SerializeField] private LeanTweenType _blackPanelFadeOUT;
    [SerializeField] private float blackPanelAnimSpeed;

    public GameObject activeColectables;
    public GameObject disabledColectables;


    private void Start()
    {
        panel.SetActive(false);

        if(_teleportButton != null)
        {
            _teleportButton.transform.localScale = new Vector3(0, 0, 0);
        }
       
        if(_teleportMenu != null)
        {
            _teleportMenu.transform.localScale = new Vector3(0, 0, 0);
        }

        FadeOUT();
        
    }

    public void FadeIN()
    {
        LeanTween.alphaCanvas(blackPannel, 1, blackPanelAnimSpeed).setEase(_blackPanelFadeIN);
    }
    public void FadeOUT()
    {
        LeanTween.alphaCanvas(blackPannel, 0, blackPanelAnimSpeed).setEase(_blackPanelFadeOUT);
    }

    public void PlayerDeathFadeIN()
    {
        LeanTween.alphaCanvas(blackPannel, 1, 3).setEase(_blackPanelFadeIN).setOnComplete(GoOtherScene);
    }

    private void GoOtherScene()
    {
        if (SceneManager.GetActiveScene().name == "Oshi_Tutorial")
        {
            SceneManager.LoadScene("DiedTutorialScreen");
        }
        else if (SceneManager.GetActiveScene().name == "Oshi")
        {
            SceneManager.LoadScene("DiedScreen");
        }
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
