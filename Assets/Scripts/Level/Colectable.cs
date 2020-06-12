using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectable : MonoBehaviour
{
    private SavedData _savedDataReference;
    private int colectedColectables = 0;

    void Start()
    {
        _savedDataReference = GameObject.Find("SaveData").GetComponent<SavedData>();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && this.gameObject.GetComponent<Collider2D>() is EdgeCollider2D)
        {

            _savedDataReference.ActivateColectable(colectedColectables);
            Debug.Log("enteredColectable number" + (colectedColectables));
            Destroy(this.gameObject);
            colectedColectables++;
        }

    }

}
