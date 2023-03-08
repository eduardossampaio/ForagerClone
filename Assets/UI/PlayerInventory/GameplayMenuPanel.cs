using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMenuPanel : MonoBehaviour
{
    [Header("Panels")]
    public GameObject[] allPanels;
    public InventoryPanel inventoryPanel;


    public void ShowInventory(Inventory inventory)
    {
        OpenTab(0);
        inventoryPanel.ShowInventory(inventory);

    }
    //TODO sumir com isso
    public void ShowItemInfo(Item item)
    {
        inventoryPanel.ShowItemInfo(item);
    }

    public void HideItemInfo()
    {
        inventoryPanel.HideItemInfo();
    }

    public void OpenTab(int tabIndex)
    {
        foreach (var panel in allPanels)
        {
            panel.SetActive(false);
        }
        allPanels[tabIndex].SetActive(true);
    }
}
