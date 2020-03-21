using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingMapScreen : MonoBehaviour
{
    public float  timeForNextPhrase = 2f;
    private float _timer;
    public List<string> phrases;
    public TMP_Text textToChange;

    private void Start()
    {
        _timer = timeForNextPhrase;
        textToChange.text = phrases[Random.Range(0, phrases.Count)];
    }

    private void FixedUpdate()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            //change phrase
            textToChange.text = phrases[Random.Range(0, phrases.Count)];
            _timer = timeForNextPhrase;
        }
    }
}
