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
    public bool benchSitting = false;
    public GameObject bench;

    void Start()
    {
        canMove = true;
        isSitting = false;
    }

    public void ResetCamera()
    {
        xRotation = 0f;
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
                xRotation = Mathf.Clamp(xRotation, -60f, 60f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
            else
            {
                xRotation = Mathf.Clamp(xRotation, -60f, 40f);
                currentRotation.y = Mathf.Clamp(currentRotation.y, -110f, 110f);

                transform.localRotation = Quaternion.Euler(xRotation, currentRotation.y, 0f);
            }
        }

        if (benchSitting && !isSitting)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, bench.transform.position, 1f * Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, bench.transform.rotation, 1f * Time.deltaTime);
            
            Debug.Log(gameObject.transform.position.x - bench.transform.position.x);
            
            if (Mathf.Abs(gameObject.transform.position.x - bench.transform.position.x) < 0.001f)
            {
                gameObject.transform.parent = bench.transform;
                
                //gameObject.transform.rotation = bench.transform.rotation;
                //gameObject.transform.position = bench.transform.position;
                
                xRotation = 0;
                currentRotation.y = 0;
                
                PlayerController.instance.canMove = true;
                isSitting = true;
                Debug.Log("Можно двигаться");
                
                SteamAPI.instance.TriggerAchievement("EndGame");
            }
        }
    }

    public void SitOnBench()
    {
        benchSitting = true;
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
