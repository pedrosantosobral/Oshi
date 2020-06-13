using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField] private VoidEvent flyAtack;
    [SerializeField] private GameObject deathFB;

    enum destroy
    {
        me,
        collidedObj,
        parentOfCollidedObj,
        referenceObject
    }

    [SerializeField]
    private string selectedTag;

    [SerializeField]
    private destroy destroyCollidedObject;
    [SerializeField]
    private GameObject objectToReference;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(selectedTag))
        {
            if(flyAtack != null)
            {
                flyAtack.Raise();
            }
            
            switch (destroyCollidedObject)
            {
                case destroy.me:
                    {
                        Instantiate(deathFB, gameObject.transform.position, Quaternion.identity);
                        Destroy(this.gameObject);
                        break;
                    }
                case destroy.referenceObject:
                    {
                        Instantiate(deathFB, objectToReference.transform.position, Quaternion.identity);
                        Destroy(objectToReference);
                        break;
                    }

                case destroy.collidedObj:
                    {
                        Instantiate(deathFB, other.gameObject.transform.position, Quaternion.identity);
                        Destroy(other.gameObject);
                        break;
                    }

                case destroy.parentOfCollidedObj:
                    {
                        Instantiate(deathFB, other.transform.parent.position, Quaternion.identity);
                        Destroy(other.transform.parent.gameObject);
                        break;
                    }
            }
        }
    }
}
