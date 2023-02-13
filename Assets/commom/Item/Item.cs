using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable/item", order = 1)]
public class Item : ScriptableObject
{
    [Header("properties")]
    public ItemType type;
    public ItemCategory category;
    public string itemName;
    public Sprite image;
    [TextArea(1,4)]
    public string description;
    [TextArea(1, 4)]
    public string shortDescription;
    public GameObject lootPrefab;
    
    [Header("loot")]
    public int hitAmount;
    public int lootAmount;

    [Header("status")]
    public int energyAmount;
    //public int manaAmount;

}