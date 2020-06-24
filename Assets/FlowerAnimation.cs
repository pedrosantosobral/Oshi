﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerInside"))
        {
            animator.SetTrigger("flower");
        }
    }
}
