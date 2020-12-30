using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public GameObject theGirl;
    public int buttonsCounter = 0;
    private bool isGirlExist = false;

    public GameObject[] pointLights;

    void Update()
    {
        if ((buttonsCounter >= 14) && (isGirlExist == false))
        {
            theGirl.SetActive(true);
            isGirlExist = true;

            for (int i = 0; i < pointLights.Length; i++)
            {
                pointLights[i].SetActive(false);
            }
            
            Debug.Log("Девушка была создана!");
        }
    }
}
