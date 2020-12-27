using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public Interactable data;
    public Transform spawnPos;

    private GameObject player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    public void ButtonPressed()
    {
        Debug.Log("Нажал на кнопку");
        player.transform.position = spawnPos.transform.position;
        
        // Запускам триггер для анимации
    }

    private void ButtonAnimation()
    {
        
    }
}
