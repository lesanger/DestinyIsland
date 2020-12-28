using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public GameObject theGirl;
    public int buttonsCounter = 0;

    void Update()
    {
        if (buttonsCounter >= 14)
        {
            theGirl.SetActive(true);
            Debug.Log("Девушка была создана!");
        }
    }
}
