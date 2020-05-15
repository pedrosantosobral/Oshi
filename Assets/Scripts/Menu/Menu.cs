using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomEventSystem;

public class Menu : MonoBehaviour {

    [SerializeField] private CanvasGroup creditsPanel;
    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private CanvasGroup blackPanel;

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
        SceneManager.LoadScene("Oshi_Tutorial");
    }

    public void PlayLvl2()
    {
        SceneManager.LoadScene("Oshi");
    }

    private void Start()
    {
        LeanTween.alphaCanvas(blackPanel,0,0);
        LeanTween.alphaCanvas(creditsPanel, 0, 0);
        LeanTween.scale(optionsPanel,new Vector3(0f,0f,0f), 0);

    }

}
