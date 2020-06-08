using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiedScreenCanvasManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackPannel;

    [SerializeField] private LeanTweenType blackPanelINAnim;

    public void FadeIN()
    {
        LeanTween.alphaCanvas(blackPannel, 0, 1).setEase(blackPanelINAnim);
    }

    private void Start()
    {
        FadeIN();
    }
}
