using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public Interactable data;
    public Transform spawnPos;
    
    private GameObject player;
    private GameObject mainCamera;
    public GameObject model;
    private GameObject buttonsManager;

    private float countToDeath = 3f;
    private float inWhatTimeDie;
    private bool timeToDeath = false;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
        buttonsManager = GameObject.FindWithTag("ButtonsManager");
    }

    private void Update()
    {
        if (timeToDeath == true )
        {
            if (Time.time > inWhatTimeDie)
            {
                Destroy(gameObject);
            }
        }
    }

    public void TalkIsDone()
    {
        Debug.Log("Разговор окончен");
        model.SetActive(true);
        Destroy(gameObject);
    }

    public void ButtonPressed()
    {
        player.transform.position = spawnPos.transform.position;
        player.transform.eulerAngles = spawnPos.transform.rotation.eulerAngles;

        // Запускам триггер для анимации лифта HUD

        if (mainCamera.TryGetComponent<CameraConroller>(out CameraConroller cameraController))
        {
            cameraController.ResetCamera();
        }

        if (buttonsManager.TryGetComponent<ButtonsManager>(out ButtonsManager manager))
        {
            manager.buttonsCounter += 1;
            Debug.Log("Количество сгоревших кнопок равно: " + manager.buttonsCounter);
        }

        timeToDeath = true;
        inWhatTimeDie = Time.time + countToDeath;
    }
}
