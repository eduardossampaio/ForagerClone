using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvents
{
    public static string USE_ITEM = "ItemEvents.USE_ITEM";
    public static string DELETE_ITEM = "ItemEvents.DELTE_ITEM";
}
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