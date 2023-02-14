using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WOOD,COAL, IRON, STONE, FOOD
}

public enum ItemCategory
{
    MATERIAL, CONSUMABLE
}
public class CoreGame : MonoBehaviour
{
    public static CoreGame instance;
    public Player player;

    public GameManager gameManager;
    public InventoryPanel inventoryPanel;

    private void Awake()
    {
        instance = this;
    }


    public void ShowInventory(Inventory inventory)
    {
        bool isActive = !inventoryPanel.gameObject.activeSelf;
        inventoryPanel.gameObject.SetActive(isActive);
        if (isActive)
        {
            inventoryPanel.ShowInventory(inventory);
        }
    }


}
