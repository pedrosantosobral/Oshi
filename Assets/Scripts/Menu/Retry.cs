using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void LoadScnene()
    {
        SceneManager.LoadScene("Oshi");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu 1");
    }
}
