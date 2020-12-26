using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public bool isSitting;
    private Vector2 currentRotation;

    public Transform playerBody;

    float xRotation = 0f;

    public bool canMove;

    void Start()
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        isSitting = false;
    }

    void Update()
    {
        if (canMove)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            currentRotation.y += mouseX;

            if (!isSitting)
            {
                xRotation = Mathf.Clamp(xRotation, -40f, 70f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
            else
            {
                xRotation = Mathf.Clamp(xRotation, -25f, 10f);
                currentRotation.y = Mathf.Clamp(currentRotation.y, -60f, 60f);

                transform.localRotation = Quaternion.Euler(xRotation, currentRotation.y, 0f);
            }
        }
    }

    public void StandUp()
	{
        xRotation = 0f;
	}

    public void SitDown()
    {
        xRotation = 0;
        currentRotation.y = 0;
    }
}
