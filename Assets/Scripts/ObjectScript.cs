using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public Interactable data;
    public Transform spawnPos;
    
    private GameObject player;
    private GameObject camera;

    private float countToDeath = 3f;
    private float inWhatTimeDie;
    private bool timeToDeath = false;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        camera = GameObject.FindWithTag("MainCamera");
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
        // Удаляем NPC и даем отмашку менеджеру сцены на включение объекта по ID
        Debug.Log("Разговор окончен");
        Destroy(gameObject);
    }

    public void ButtonPressed()
    {
        player.transform.position = spawnPos.transform.position;
        //Vector3 newRot = spawnPos.transform.rotation.eulerAngles;
        player.transform.eulerAngles = spawnPos.transform.rotation.eulerAngles;

        // Запускам триггер для анимации лифта HUD

        if (camera.TryGetComponent<CameraConroller>(out CameraConroller cameraController))
        {
            cameraController.ResetCamera();
        }

        timeToDeath = true;
        inWhatTimeDie = Time.time + countToDeath;
    }
}
