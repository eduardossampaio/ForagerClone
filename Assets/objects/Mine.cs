using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public Item item;
    private int hitAmount;
    private void Start()
    {
        hitAmount = item.hitAmount;
    }
    private void OnMouseOver()
    {
        CoreGame.instance.gameManager.ActiveCursor(gameObject);
    }

    private void OnMouseExit()
    {
        CoreGame.instance.gameManager.DisableCursor();
    }

    private void OnHit()
    {
        hitAmount--;
        if (hitAmount <= 0)
        {
            CoreGame.instance.gameManager.DisableCursor();
            Loot();
            Destroy(gameObject);
        }
    }

    public void Loot()
    {
        int dir = 1;
        for (int i = 0; i < item.lootAmount; i++)
        {
            var loot = Instantiate(item.lootPrefab,transform.position,transform.localRotation);
            loot.transform.position = transform.position;
            dir *= -1;
            loot.SendMessage("Active", dir, SendMessageOptions.DontRequireReceiver);
        }
    }
}
