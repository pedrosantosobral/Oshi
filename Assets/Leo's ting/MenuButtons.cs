using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{

    public GameObject Screen;

    public GameObject Playbutton;
    public GameObject Backbutton;


    private void Start()
    {
        Playbutton.SetActive(true);
        Backbutton.SetActive(false);
    }
     public void Screen1()
     {
         Screen.transform.Translate(2000f, 0, 0);
         Playbutton.SetActive(true);
         Backbutton.SetActive(false);
     }

     public void Screen2()
     {
         Screen.transform.Translate(-2000f, 0, 0);
         Playbutton.SetActive(false);
         Backbutton.SetActive(true);
     }
     

}
