using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OshiTEXTAnimation : MonoBehaviour
{
    [SerializeField] private LeanTweenType textAnimation;
    [SerializeField] private float animationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(3f, 1.5f, 0), animationSpeed).setEase(textAnimation);   
    }
}
