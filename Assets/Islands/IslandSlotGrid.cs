using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSlotGrid : MonoBehaviour
{
    public int line;
    public int col;
    public bool isBusy;

    public Collider2D slotCollider;
    public GameConfigurations configurations;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetBusy(bool busy)
    {
        this.isBusy = busy;
        slotCollider.enabled = !busy;
    }

    public void ShowBorder(bool show)
    {
        spriteRenderer.enabled = show;
    }

    private void OnMouseDown()
    {
        if(configurations.gameState == GameState.CRAFTING && !isBusy)
        {
            CoreGame.instance.SetCraftObject(this);
        }
    }
}
