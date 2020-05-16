using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightInfor : MonoBehaviour
{

    [SerializeField] private LeanTweenType animWobleOut;
    [SerializeField] private LeanTweenType animWobleIN;
    [SerializeField] private float woblespeed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Trap"))
        {
            collision.gameObject.GetComponent<Trap>()._isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            collision.gameObject.GetComponent<Trap>()._isActive = false;
        }
    }

    private void Start()
    {
        WobleOUT();
    }

    private void WobleIn()
    {
        woblespeed = Random.Range(0.2f, 0.5f);
        LeanTween.moveLocal(gameObject, new Vector3(0, 0.367f, 0), woblespeed).setEase(animWobleIN).setOnComplete(WobleOUT);
    }

    private void WobleOUT()
    {
        woblespeed = Random.Range(0.2f, 0.5f);
        LeanTween.moveLocal(gameObject, new Vector3(Random.Range(-0.03f,0.03f), Random.Range(-0.03f, 0.03f) + 0.367f, 0), woblespeed).setEase(animWobleIN).setOnComplete(WobleOUT);
        
    }

}
