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

public enum GameState
{
    GAMEPLAY, INVENTORY, CRAFTING
}

static class Extensions
{
    public static string Description(this ItemCategory category)
    {
        switch (category)
        {
            case ItemCategory.MATERIAL: return "Material";
            case ItemCategory.CONSUMABLE: return "Consumível";
        }
        return "";
    }
}

public class CoreGame : MonoBehaviour
{
    public static CoreGame instance;
    public Player player;
    public GameConfigurations configurations;
    public GameplayMenuPanel gameplayMenuPanel;

    public GameObject craftObject;
    private void Awake()
    {
        instance = this;
    }


    public void ShowInventory(Inventory inventory)
    {
        bool isActive = !gameplayMenuPanel.gameObject.activeSelf;
        gameplayMenuPanel.gameObject.SetActive(isActive);
        if (isActive)
        {
            gameplayMenuPanel.ShowInventory(inventory);
            configurations.ChangeState(GameState.INVENTORY);
        }
        else
        {
            configurations.ChangeState(GameState.GAMEPLAY);
        }
    }

    public float DistanceToPlayer(GameObject comparingObject)
    {
        return Vector3.Distance(player.transform.position, comparingObject.transform.position);
    }

    //TODO muda pro slot
    public void SetCraftObject(IslandSlotGrid slot)
    {
        GameObject objCraft = Instantiate(craftObject);
        objCraft.transform.position = slot.transform.position;
        slot.SetBusy(true);
        slot.ShowBorder(false);

        configurations.ChangeState(GameState.GAMEPLAY);
    }

    public void GoToCraftMode(GameObject objectToCraft)
    {
        craftObject = objectToCraft;
        configurations.ChangeState(GameState.CRAFTING);
    }

}
