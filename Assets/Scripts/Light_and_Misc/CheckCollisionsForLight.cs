using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionsForLight : MonoBehaviour
{
    [SerializeField] private LeanTweenType animIN;
    [SerializeField] private LeanTweenType animOUT;


    public Collider2D thisCollider;
    public bool notInsideWall = false;
    public LayerMask walls;
    private float _overlapWallsRad = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 0), 0.15f).setEase(animIN);
        notInsideWall = !Physics2D.OverlapCircle(gameObject.transform.position, _overlapWallsRad, walls);
        if(notInsideWall)
        {
            thisCollider.enabled = true;
        }
        
    }

    public void AnimOUT()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.25f).setEase(animOUT).setOnComplete(DestroyGameObject);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    
}
