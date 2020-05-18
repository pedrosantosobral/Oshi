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
    bool stopTimmer = false;

    [SerializeField] private LeanTweenType IN_ease_type;
    [SerializeField] private float IN_time;

    private void Start()
    {
        textToChange.gameObject.transform.localScale = Vector3.zero;
        LeanTween.scale(textToChange.gameObject, new Vector3(1, 1, 1), IN_time).setEase(IN_ease_type);
        _timer = timeForNextPhrase;
        textToChange.text = phrases[Random.Range(0, phrases.Count)];
    }

    private void FixedUpdate()
    {
        if(stopTimmer == false)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                //change phrase
                textToChange.gameObject.transform.localScale = Vector3.zero;
                string temp = phrases[Random.Range(0, phrases.Count)];
                textToChange.text = temp;
                if (phrases.Count <= 5)
                {
                    phrases.Remove(temp);
                    stopTimmer = true;
                }
                LeanTween.scale(textToChange.gameObject, new Vector3(1, 1, 1), IN_time).setEase(IN_ease_type);
                _timer = timeForNextPhrase;
            }
        }
        
    }

    private void ScaleTo()
    {
        textToChange.gameObject.transform.localScale = Vector3.zero;
    }
}
