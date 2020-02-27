using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    public GameObject[] colectablesToActivate;

    public void ActivateColectable(int colectable)
    {
        colectablesToActivate[colectable].SetActive(true);
    }
        
}
