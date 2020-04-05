using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitDetection : MonoBehaviour
{
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
