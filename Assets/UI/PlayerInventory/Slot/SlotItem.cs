using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SimpleEventSystem;
public class SlotItem : MonoBehaviour
{
    public Image itemImage;
    public Text amountText;

    public Image deleteBarBg;
    public Image deleteBar;

    private Item item;
    private bool isDeletingItem;

    public GameConfigurations configurations;

    private float deltaTime;
    [SerializeField] private NotificationAgent notificationAgent;

    private void Update()
    {
        if (isDeletingItem)
        {
            float timeToDelete = configurations.timeToDeleteItemInInventory;
            deltaTime += Time.deltaTime;
            var percentual = deltaTime / timeToDelete;
            deleteBar.fillAmount = percentual;

            if(deltaTime >= timeToDelete)
            {
                DeleteItem();
            }
        }
    }
    public void UpdateSlot(Item item, int amount)
    {
        this.item = item;
        itemImage.sprite = item.image;
        amountText.text = amount.ToString();
        ShowDeleteBar(false);
    }

    public void OnSlotClick(BaseEventData baseEventData)
    {
        var pointerEventData = baseEventData as PointerEventData;

        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            UseItem();
        }
        else if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            isDeletingItem = true;
            deltaTime = 0;
            deleteBar.fillAmount = 0.1f;
            ShowDeleteBar(true);
        }
    }

    public void OnSlotUp(BaseEventData baseEventData)
    {
        var pointerEventData = baseEventData as PointerEventData;
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            isDeletingItem = false;
            ShowDeleteBar(false);
        }
    }

    public void MouseEnter()
    {
        CoreGame.instance.gameplayMenuPanel.ShowItemInfo(item);
    }

    public void MouseExit()
    {
        CoreGame.instance.gameplayMenuPanel.HideItemInfo();
        isDeletingItem = false;
        ShowDeleteBar(false);
    }

    private void ShowDeleteBar(bool show)
    {
        deleteBarBg.gameObject.SetActive(show);
        deleteBar.gameObject.SetActive(show);
    }

    private void DeleteItem()
    {
        notificationAgent.NotifyEvent(ItemEvents.DELETE_ITEM, item);
    }

    private void UseItem()
    {
        if (item.category == ItemCategory.CONSUMABLE)
        {
            notificationAgent.NotifyEvent(ItemEvents.USE_ITEM, item);
        }
    }
}
