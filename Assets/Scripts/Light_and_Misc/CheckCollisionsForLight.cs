using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionsForLight : MonoBehaviour
{
    public Collider2D thisCollider;
    public bool notInsideWall = false;
    public LayerMask walls;
    private float _overlapWallsRad = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        notInsideWall = !Physics2D.OverlapCircle(gameObject.transform.position, _overlapWallsRad, walls);
        if(notInsideWall)
        {
            thisCollider.enabled = true;
        }
    }
}
