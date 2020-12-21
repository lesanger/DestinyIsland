using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerText : MonoBehaviour
{
    public GameObject playerCamera;

    void Update()
    {
        this.transform.rotation = Quaternion.LookRotation(this.transform.position - playerCamera.transform.position);
        //this.transform.Rotate(0f, 180f, 0f);
    }
}
