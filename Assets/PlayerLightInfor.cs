using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightInfor : MonoBehaviour
{
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

}
