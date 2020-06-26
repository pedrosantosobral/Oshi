using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomEventSystem;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    private bool toggle;
    [SerializeField] private Sprite muted;
    [SerializeField] private Sprite not_muted;
    [SerializeField] private Image SoundButton;

    [SerializeField] private AudioClip popIN;
    [SerializeField] private AudioClip popOUT;

    [SerializeField] private CanvasGroup creditsPanel;
    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private CanvasGroup blackPanel;
    [SerializeField] private CanvasGroup fadeBlackPanel;

    [SerializeField] private float blackPanelFadeINTime;
    [SerializeField] private float blackPanelFadeOUTTime;
    [SerializeField] private float rollTextSpeed;
    [SerializeField] private LeanTweenType blackPanelINAnim;
    [SerializeField] private LeanTweenType blackPanelOUTAnim;

    [SerializeField] private float optionsFadeINTime;
    [SerializeField] private float optionsFadeOUTTime;
    [SerializeField] private LeanTweenType optionsINAnim;
    [SerializeField] private LeanTweenType optionsOUTAnim;

    int id;

    public void RollText()
    {
       id = LeanTween.moveLocalY(creditsPanel.gameObject, 2030f, rollTextSpeed).id;
    }

    public void ResetRollText()
    {
        LeanTween.cancel(id);
        LeanTween.moveLocalY(creditsPanel.gameObject, 0f, 0f);
    }

    public void CreditsFadeIn()
    {
        blackPanel.blocksRaycasts = true;
        LeanTween.alphaCanvas(blackPanel, 1, blackPanelFadeINTime).setEase(blackPanelINAnim).setOnComplete(CreditsTextFadeIn);
    }

    public void CreditsFadeOut()
    {
        LeanTween.alphaCanvas(blackPanel, 0, blackPanelFadeOUTTime).setEase(blackPanelOUTAnim).setOnComplete(ResetRollText);
        blackPanel.blocksRaycasts = false;
    }

    public void CreditsTextFadeIn()
    {
        LeanTween.alphaCanvas(creditsPanel, 1, blackPanelFadeINTime).setEase(blackPanelINAnim).setOnComplete(RollText);
    }

    public void OptionsAnimIn()
    {
        LeanTween.scale(optionsPanel, new Vector3(0.434f,0.434f,0.434f), optionsFadeINTime).setEase(optionsINAnim);
    }

    public void OptionsAnimOut()
    {
        LeanTween.scale(optionsPanel, new Vector3(0,0,0), optionsFadeOUTTime).setEase(optionsOUTAnim);
    }

    public void PlayLvl1()
    {
        LeanTween.alphaCanvas(fadeBlackPanel, 1, blackPanelFadeINTime).setEase(blackPanelINAnim).setOnComplete(LoadLvl1);

    }

    public void PlayLvl2()
    {
        LeanTween.alphaCanvas(fadeBlackPanel, 1, blackPanelFadeINTime).setEase(blackPanelINAnim).setOnComplete(LoadLvl2);

    }

    public void PlayLvl3()
    {
        LeanTween.alphaCanvas(fadeBlackPanel, 1, blackPanelFadeINTime).setEase(blackPanelINAnim).setOnComplete(LoadLvl3);
    }

    public void PlayLvl4()
    {
        LeanTween.alphaCanvas(fadeBlackPanel, 1, blackPanelFadeINTime).setEase(blackPanelINAnim).setOnComplete(LoadLvl4);
    }

    public void PlayLvl5()
    {
        LeanTween.alphaCanvas(fadeBlackPanel, 1, blackPanelFadeINTime).setEase(blackPanelINAnim).setOnComplete(LoadLvl5);
    }

    public void PlayLvl6()
    {
        LeanTween.alphaCanvas(fadeBlackPanel, 1, blackPanelFadeINTime).setEase(blackPanelINAnim).setOnComplete(LoadLvl6);
    }

    public void PlayLvl7()
    {
        LeanTween.alphaCanvas(fadeBlackPanel, 1, blackPanelFadeINTime).setEase(blackPanelINAnim).setOnComplete(LoadLvl7);
    }

    private void Start()
    {
        toggle = false;
        AudioManager.Instance.PlaySFX(popIN);
        LeanTween.alphaCanvas(blackPanel,0,0);
        LeanTween.alphaCanvas(creditsPanel, 0, 0);
        LeanTween.scale(optionsPanel,new Vector3(0f,0f,0f), 0);
        AudioManager.Instance.PlaySFX(popIN);
    }

    private void LoadLvl1()
    {
        SceneManager.LoadScene("Oshi_Tutorial 1");
    }

    private void LoadLvl2()
    {
        SceneManager.LoadScene("Oshi_Tutorial 2");
    }

    private void LoadLvl3()
    {
        SceneManager.LoadScene("Oshi_Tutorial 3");
    }

    private void LoadLvl4()
    {
        SceneManager.LoadScene("Oshi_Tutorial 4");
    }
    private void LoadLvl5()
    {
        SceneManager.LoadScene("Oshi_Tutorial 5");
    }

    private void LoadLvl6()
    {
        SceneManager.LoadScene("Oshi_Tutorial 6");
    }

    private void LoadLvl7()
    {
       SceneManager.LoadScene("Oshi");
    }

    public void ButtonIN()
    {
        AudioManager.Instance.PlaySFX(popIN);
    }
    public void ButtonOUT()
    {
        AudioManager.Instance.PlaySFX(popOUT);
    }

    public void AudioControl()
    {
        if (AudioListener.volume == 0f)
       {
            AudioListener.volume = 1;
            
       }
       else
       {
            AudioListener.volume = 0f;
            
        }
    }

    public void ToggleSound()
    {
        
        toggle = !toggle;

        if (toggle)
        {
            AudioListener.volume = 1f;
            SoundButton.sprite = not_muted;
        }
        else
        {
            AudioListener.volume = 0f;
            SoundButton.sprite = muted;
        }
            
    }

}
