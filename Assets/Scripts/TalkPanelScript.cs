using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkPanelScript : MonoBehaviour
{
    public string sinName;

    public void TalkStart()
    {
        PlayerController.instance.Talk();
    }

    public void PositiveTalk()
    {
        PlayerController.instance.PositiveTalk();
    }

    public void NegativeTalk()
    {
        PlayerController.instance.NegativeTalk();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
