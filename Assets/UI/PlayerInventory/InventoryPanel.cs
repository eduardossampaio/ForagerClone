using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [Header("Inventory Panel")]
    public RectTransform slotGrid;
    public GameObject slotPrefab;

    [Header("Info Panel")]
    public InfoPanel panelInfo;


    private List<GameObject> inventorySlots = new List<GameObject>();


    public void ShowInventory(Inventory inventory)
    {
        HideItemInfo();
        UpdateInventory(inventory);
    }

    public void UpdateInventory(Inventory inventory)
    {
        HideItemInfo();
        foreach (GameObject slots in inventorySlots)
        {
            Destroy(slots);
        }
        inventorySlots.Clear();
        foreach (var item in inventory.items)
        {
            var slot = Instantiate(slotPrefab, slotGrid);
            slot.GetComponent<SlotItem>().UpdateSlot(item.Key, item.Value);
            inventorySlots.Add(slot);
        }
    }

    public void ShowItemInfo(Item item)
    {
        panelInfo.gameObject.SetActive(true);
        panelInfo.ShowItemInfo(item);
    }

    public void HideItemInfo()
    {
        panelInfo.gameObject.SetActive(false);
    }

}
