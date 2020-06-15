using UnityEngine;
using CustomEventSystem;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private VoidEvent onPlayerDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.isTrigger)
        {
            if (other.CompareTag("PlayerInside"))
            {
                //SEND EVENT NOTIFICATION TO PLAYER SAYING HE WAS HIT
                onPlayerDamage.Raise();
            }
        }

     
    }
 
}