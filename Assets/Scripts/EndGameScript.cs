using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    public GameObject nameText;
    public GameObject thanksText;
    public GameObject leaveText;

    public void EndGameScreen()
    {
        PlayerController.instance.canMove = false;
        
        nameText.SetActive(true);
        thanksText.SetActive(true);
        leaveText.SetActive(true);
    }
}
