using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLightSprite : MonoBehaviour
{
    public Sprite[]     lightSprites;
    private SpriteMask _spriteRenderReference;
    void Start()
    {
        _spriteRenderReference = gameObject.GetComponent<SpriteMask>();
        _spriteRenderReference.sprite = lightSprites[Random.Range(0, lightSprites.Length)];
    }

    
}
