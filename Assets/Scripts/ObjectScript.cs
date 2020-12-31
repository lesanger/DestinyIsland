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
    
    public GameObject buttonPanel;

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

    // Вызывается из PlayerController после УСПЕШНОГО разговора с NPC
    public void TalkIsDone()
    {
        if (data.id != 73)
        {
            model.SetActive(true);
            Destroy(gameObject);
        }
        
        // Если NPC - девушка
        if (data.id == 73)
        {
            // Запустить с анимацию выхода из игры
            UI.instance.SetEndGameScreen();
        }
    }

    // Вызывается из PlayerController при взаимодействии с КНОПКОЙ
    public void ButtonPressed()
    {
        // Анимация лифта
        Animation anim = buttonPanel.GetComponent<Animation>();
        ButtonPanelScript buttonPanelScript = buttonPanel.GetComponent<ButtonPanelScript>();
        buttonPanelScript.spawnPosition = spawnPos;
        anim.Play();

        // Счетчик использованных кнопок
        if (buttonsManager.TryGetComponent<ButtonsManager>(out ButtonsManager manager))
        {
            manager.buttonsCounter += 1;
            Debug.Log("Количество сгоревших кнопок равно: " + manager.buttonsCounter);
        }

        // Убираем использованную кнопку
        timeToDeath = true;
        inWhatTimeDie = Time.time + countToDeath;
    }
}
