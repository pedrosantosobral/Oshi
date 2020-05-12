using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] sprites;

    private void Start()
    {
        int i = Random.Range(0, sprites.Length);

        SpriteRenderer spriteComp = GetComponent<SpriteRenderer>();

        spriteComp.sprite = sprites[i];
    }
}
