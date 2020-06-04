using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTips : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private LeanTweenType textIN;
    [SerializeField] private LeanTweenType textOUT;
    [SerializeField] private float animSpeed;

    private void Start()
    {
        LeanTween.scale(text, new Vector3(0, 0, 0), 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        LeanTween.scale(text, new Vector3(1, 1, 0), animSpeed).setEase(textIN);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        LeanTween.scale(text, new Vector3(0, 0, 0), animSpeed).setEase(textOUT);
    }


}
