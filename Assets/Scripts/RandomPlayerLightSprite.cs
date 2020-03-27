using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerLightSprite : MonoBehaviour
{
    public Sprite[]     lightSprites;
    private SpriteMask _spriteRenderReference;
    public float timeForNextSprite = 2f;
    private float _timer;

    private void Start()
    {
        _timer = timeForNextSprite;
        _spriteRenderReference = gameObject.GetComponent<SpriteMask>();
    }
    private void FixedUpdate()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            //change sprite
            _spriteRenderReference.sprite = lightSprites[Random.Range(0, lightSprites.Length)];
            _timer = Random.Range(0.01f, 0.1f);
        }
    }


}
