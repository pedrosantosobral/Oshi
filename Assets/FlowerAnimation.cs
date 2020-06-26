using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Animation()
    {
        animator.SetTrigger("flower");
    }
}
