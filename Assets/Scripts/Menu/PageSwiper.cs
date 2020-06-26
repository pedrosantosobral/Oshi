using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private AudioClip swipeSound;
    [SerializeField] private float swipe_volume;

    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 1;
    private int panelAmount = 3;
    private int actualPanel = 1;

    // Start is called before the first frame update
    void Start()
    {
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        float difference;

        difference = data.pressPosition.x - data.position.x;

        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        Debug.Log(percentage);
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && actualPanel < panelAmount)
            {
                newLocation += new Vector3(-Screen.width, 0, 0);
                actualPanel += 1;
            }
            else if (percentage < 0 && actualPanel > 1)
            {
                newLocation += new Vector3(Screen.width, 0, 0);
                actualPanel -= 1;
            }

            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            AudioManager.Instance.PlaySFX(swipeSound,swipe_volume);
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
            AudioManager.Instance.PlaySFX(swipeSound,swipe_volume);
        }
       
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while(t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 2f, t));
            yield return null;
        }
    }
}
