using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data", order = 1)]

public class Interactable : ScriptableObject
{
    [Header("Тип объекта")]
    public Type type;
    
    [Header("Данные NPC")]
    public int id; // Используются для подсчета выполненных грехов
    public new string name;
    public Sprite artwork;
}
public enum Type { chair, npc, button };
