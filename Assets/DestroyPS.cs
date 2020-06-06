using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPS : MonoBehaviour
{
    [SerializeField] private LeanTweenType animiIN;
    [SerializeField] private LeanTweenType animOUT;
    [SerializeField] private float animINSPEED;
    [SerializeField] private float animOUTSPEED;

    void Start()
    {
        AnimIN();
    }

    private void AnimIN()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), animINSPEED).setEase(animiIN).setOnComplete(AnimOUT);
    }
    private void AnimOUT()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), animOUTSPEED).setEase(animOUT).setOnComplete(DestroyThis);
    }
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
