using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventSystem;

public class Mine : MonoBehaviour
{
    public static class MineEvents
    {
        public static string OnSelected     = "MineEvents.OnSelected";
        public static string OnDeselected   = "MineEvents.OnDeselected";
        public static string OnDestroyed    = "MineEvents.OnDestroyed";
    }
    public Item item;
    public GameConfigurations configurations;

    private int hitAmount;
    private NotificationAgent notificationAgent;
    private bool isSelected = false;
    private void Start()
    {
        hitAmount = item.hitAmount;
        notificationAgent = GetComponent<NotificationAgent>();
        notificationAgent.RegisterEvent<Player>(Player.PlayerEvents.PLAYER_HIT, OnPlayerHit);
    }
    private void OnMouseOver()
    {
        notificationAgent.NotifyEvent<Mine>(MineEvents.OnSelected, this);
        isSelected = true;
    }

    private void OnMouseExit()
    {
        notificationAgent.NotifyEvent(MineEvents.OnDeselected, this);
        isSelected = false;
    }

    private void OnHit()
    {
        hitAmount--;
        if (hitAmount <= 0)
        {
            notificationAgent.NotifyEvent(MineEvents.OnDestroyed, this);
            Loot();
            Destroy(gameObject);
        }
    }

    private void OnPlayerHit(Player player)
    {
        if(isSelected && IsNear(player))
        {
            OnHit();
        }
    }

    private bool IsNear(Player player)
    {
        return Vector2.Distance(gameObject.transform.position, player.transform.position) < configurations.mininumDistanceToInteractWithMine;
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
