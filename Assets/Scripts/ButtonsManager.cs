using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public GameObject theGirl;
    public int buttonsCounter = 0;
    private bool isGirlExist = false;

    void Update()
    {
        if ((buttonsCounter >= 14) && (isGirlExist == false))
        {
            theGirl.SetActive(true);
            isGirlExist = true;
            
            Debug.Log("Девушка была создана!");
        }
    }
}
