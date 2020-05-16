using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCamera;
    private float imageLength;
    private float imageHeight;

    private float imageStartHorizontalPosition;
    private float imageStartVerticalPosition;
    [SerializeField]
    internal float parallaxHorizontalSpeed;
    [SerializeField]
    internal float parallaxVerticalSpeed;


    void Start()
    {
        imageStartHorizontalPosition = transform.position.x;
        imageStartVerticalPosition = transform.position.y;

        imageLength = GetComponent<SpriteRenderer>().bounds.size.x;
        imageHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
        float tempX = (playerCamera.transform.position.x * (1 - parallaxHorizontalSpeed));
        float distX = (playerCamera.transform.position.x * parallaxHorizontalSpeed);

        float tempY = (playerCamera.transform.position.y * (1 - parallaxVerticalSpeed));
        float distY = (playerCamera.transform.position.y * parallaxVerticalSpeed);


        transform.position = new Vector3(imageStartHorizontalPosition - distX, imageStartVerticalPosition - distY, transform.position.z);

        if(tempX > imageStartHorizontalPosition + imageLength / 2)
        {
            imageStartHorizontalPosition += imageLength;
        }
       else if (tempX < imageStartHorizontalPosition - imageLength / 2)
        {
            imageStartHorizontalPosition -= imageLength;
        }

        if (tempY > imageStartVerticalPosition + imageHeight / 2)
        {
            imageStartVerticalPosition += imageHeight;
        }

        else if (tempY < imageStartVerticalPosition - imageHeight / 2)
        {
            imageStartVerticalPosition -= imageHeight;
        }
    }
}
