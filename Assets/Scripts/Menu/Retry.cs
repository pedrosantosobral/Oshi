using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void LoadScnene()
    {
        if (SceneManager.GetActiveScene().name == "DiedTutorialScreen")
        {
            SceneManager.LoadScene("Oshi_Tutorial");
        }
        else if(SceneManager.GetActiveScene().name == "DiedScreen")
        {
            SceneManager.LoadScene("Oshi");
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
