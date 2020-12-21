using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ObjectData", order = 1)]

public class InteractableData : ScriptableObject
{
    public Type type;
}
public enum Type { chair, item, interactable };