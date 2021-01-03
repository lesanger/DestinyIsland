using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanelScript : MonoBehaviour
{
    public Transform spawnPosition;

    public void TeleportPlayer()
    {
        PlayerController.instance.ElevatorTeleport(spawnPosition);
    }

    public void FirstTimeLoad()
    {
        UI.instance.FirstTimeLoad();
    }

    public void EnablePlayerInput()
    {
        PlayerController.instance.canInput = true;
        Debug.Log("Ввод включен");
    }
}
