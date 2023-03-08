using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventSystem;

public class InventoryPanel : MonoBehaviour
{

    [Header("Inventory Panel")]
    public RectTransform slotGrid;
    public GameObject slotPrefab;

    
    [Header("Info Panel")]
    public InfoPanel panelInfo;

    [Header("notification Event System")]
    public NotificationAgent agent;

    private NotificationAgent notificationAgent;
    private Inventory lastInventory;

    private List<GameObject> inventorySlots = new List<GameObject>();

    private void Start()
    {
        notificationAgent = GetComponent<NotificationAgent>();
        notificationAgent.RegisterEvent<Item>(ItemEvents.USE_ITEM, UseItem, NotificationEventPriority.LOWEST);
        notificationAgent.RegisterEvent<Item>(ItemEvents.DELETE_ITEM, DeleteItem, NotificationEventPriority.LOWEST);
    }
    public void ShowInventory(Inventory inventory)
    {
        lastInventory = inventory;
        HideItemInfo();
        UpdateInventory(inventory);
    }

    public void UpdateInventory(Inventory inventory)
    {
        HideItemInfo();
        if(inventory == null)
        {
            return;
        }
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

    private void UseItem(Item item)
    {
        UpdateInventory(lastInventory);
    }

    private void DeleteItem(Item item)
    {
        UpdateInventory(lastInventory);
    }

}
