using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void PlayLvl1 ()
    {
        SceneManager.LoadScene("Oshi_Tutorial");
    }

    public void PlayLvl2()
    {
        SceneManager.LoadScene("Oshi");
    }
}
