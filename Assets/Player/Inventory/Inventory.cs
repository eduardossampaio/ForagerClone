using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    public Dictionary<Item, int> items = new Dictionary<Item, int>();

    public GameObject inventoryPanel;
   
    public void AddItem(Item item, int amount)
    {
        if (items.ContainsKey(item))
        {
            items[item] += amount;
        }
        else
        {
            items.Add(item, amount);
        }
    }

    public void DeleteItem(Item item)
    {
        items.Remove(item);
    }

    public void UseItem(Item item)
    {
        int amount = items[item];
        amount--;

        if (amount <= 0)
        {
            DeleteItem(item);
        }
        else
        {
            items[item] = amount;
        }
    }

}
