using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField] private GameObject patrolDeathFB;
    [SerializeField] private GameObject jumpDeathFB;

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
    private GameObject objectoReference;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(selectedTag))
        {
            switch (destroyCollidedObject)
            {
                case destroy.me:
                    {
                        Instantiate(patrolDeathFB, gameObject.transform.position, Quaternion.identity);
                        Destroy(this.gameObject);
                        break;
                    }
                case destroy.referenceObject:
                    {
                        Destroy(objectoReference);
                        break;
                    }

                case destroy.collidedObj:
                    {
                        Instantiate(jumpDeathFB, other.gameObject.transform.position, Quaternion.identity);
                        Destroy(other.gameObject);
                        break;
                    }

                case destroy.parentOfCollidedObj:
                    {
                        Destroy(other.transform.parent.gameObject);
                        break;
                    }
            }
        }
    }
}
