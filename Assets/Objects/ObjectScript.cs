using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public InteractableData data;

    private TextMesh textMesh;
    private string objectName;

    private void Awake()
    {
        //textMesh = GetComponentInChildren<TextMesh>();
        //textMesh.text = data.type.ToString();
    }
}
