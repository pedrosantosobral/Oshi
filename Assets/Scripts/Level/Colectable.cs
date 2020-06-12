using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectable : MonoBehaviour
{
    private SavedData _savedDataReference;

    void Start()
    {
        _savedDataReference = GameObject.Find("SaveData").GetComponent<SavedData>();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerInside"))
        {
            _savedDataReference.ActivateNextColectable();
            Debug.Log("enteredColectable");
            Destroy(this.gameObject);
        }

    }

}
