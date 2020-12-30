using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkPanelScript : MonoBehaviour
{
    public string sinName;

    public void TalkStart()
    {
        Debug.Log("Анимация прошла");
        PlayerController.instance.Talk();
    }
}
