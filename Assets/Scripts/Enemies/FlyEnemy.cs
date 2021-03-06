﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using CustomEventSystem;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] private float bite_volume;
    [SerializeField] private AudioClip bite;
    [SerializeField] private Animator animator;
    public Collider2D colliderToBeCheckedByThePlayer;
    public LayerMask obstacleMask;

    private GameObject _bestTarget;
    AIDestinationSetter _aiDestinationSetterReference;

    private List<GameObject> _enemyOwnList;
    private PaintLight _paintLightComponentReference;
    // Start is called before the first frame update
    private void Start()
    {

        //Physics2D.queriesStartInColliders = false;
        _aiDestinationSetterReference = gameObject.GetComponent<AIDestinationSetter>();
        _paintLightComponentReference = GameObject.Find("PaintLight").GetComponent<PaintLight>();
        _enemyOwnList = new List<GameObject>();
    }

    private void Update()
    {
        _enemyOwnList = _paintLightComponentReference.listToEnemies;
        if(_aiDestinationSetterReference.target != null)
        {
            animator.SetTrigger("Walking");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("PlayerInside") && !other.isTrigger)
        {
            _aiDestinationSetterReference.target = other.transform;
        }


        if (other.CompareTag("hpRecharge"))
        {
            //Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("PatrolEnemy"))
        {
            _aiDestinationSetterReference.target = other.transform;
        }

        if (other.CompareTag("light"))
        {
            //_aiDestinationSetterReference.target = other.transform;
            GetClosestTarget();
            CheckForNearestTargetToFolow();

        }

    }
    private void CheckForNearestTargetToFolow()
    {
        if (_bestTarget != null)
            _aiDestinationSetterReference.target = _bestTarget.transform;
    }

    private void GetClosestTarget()
    {
       
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in _enemyOwnList)
        {

            if(potentialTarget != null)
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    _bestTarget = potentialTarget;
                }
            }
            
        }
    }

    public void Atack()
    {
        AudioManager.Instance.PlaySFX(bite,bite_volume);
        animator.SetTrigger("Atack");

    }

}
