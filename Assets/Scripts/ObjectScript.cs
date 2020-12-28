using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public Interactable data;
    public Transform spawnPos;

    private GameObject player;

    private float countToDeath = 3f;
    private float inWhatTimeDie;
    private bool timeToDeath = false;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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

    public void ButtonPressed()
    {
        Debug.Log("Нажал на кнопку");
        player.transform.position = spawnPos.transform.position;
        
        // Запускам триггер для анимации

        timeToDeath = true;
        inWhatTimeDie = Time.time + countToDeath;
    }

    private void ButtonAnimation()
    {
        
    }
}
