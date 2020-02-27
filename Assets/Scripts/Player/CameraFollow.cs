using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Update()
    {
        if (target == null){

            target = GameObject.FindWithTag("Player");
        }

    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

}


