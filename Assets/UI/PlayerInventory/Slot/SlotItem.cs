using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SlotItem : MonoBehaviour
{
    public Image itemImage;
    public Text amountText;

    public Image deleteBarBg;
    public Image deleteBar;

    private Item item;
    private bool isDeletingItem;

    private float deltaTime;

    private void Update()
    {
        if (isDeletingItem)
        {
            float timeToDelete = CoreGame.instance.gameManager.timeToDeleteItem;
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
        CoreGame.instance.inventoryPanel.ShowItemInfo(item);
    }

    public void MouseExit()
    {
        CoreGame.instance.inventoryPanel.HideItemInfo();
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
        CoreGame.instance.DeleteItem(item);
    }

    private void UseItem()
    {
        if (item.category == ItemCategory.CONSUMABLE)
        {
            CoreGame.instance.UseItem(item);
        }
    }
}
