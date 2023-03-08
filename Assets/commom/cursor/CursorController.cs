using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventSystem;
public class CursorController : MonoBehaviour
{
    [SerializeField] private GameObject actionCursor;
    [SerializeField] private float interactionDistance;

    [SerializeField] private NotificationAgent notificationAgent;

    private GameObject interactingObject;
    private float distanceToObject = float.PositiveInfinity;

    void Start()
    {
        notificationAgent.RegisterEvent<Mine>(Mine.MineEvents.OnSelected, OnSelectedMine);
        notificationAgent.RegisterEvent<Mine>(Mine.MineEvents.OnDeselected, OnDeselectedMine);
        notificationAgent.RegisterEvent<Mine>(Mine.MineEvents.OnDestroyed, OnDestroyedMine);
        notificationAgent.RegisterEvent<Player>(Player.PlayerEvents.PLAYER_MOVED, OnPlayerMoved);
    }


    private void OnSelectedMine(Mine mine)
    {
        CalculateDistance();
        ActiveCursor(mine.gameObject);
    }

    private void OnDeselectedMine(Mine mine)
    {
        DisableCursor();
    }

    private void OnDestroyedMine(Mine mine)
    {
        if(mine.gameObject == interactingObject)
        {
            DisableCursor();
        }
    }

    private void OnPlayerMoved(Player player)
    {
        CalculateDistance();
    }

    private void CalculateDistance()
    {
        if (interactingObject == null)
        {
            return;
        }

        //distanceToObject = Vector2.Distance(CoreGame.instance.player.transform.position, interactingObject.transform.position);
        distanceToObject = CoreGame.instance.DistanceToPlayer(interactingObject);
        if (distanceToObject <= interactionDistance)
        {
            ActiveCursor(interactingObject);
        }
        else
        {
            DisableCursor();
        }
    }

    private void ActiveCursor(GameObject obj)
    {
        interactingObject = obj;
        if (distanceToObject <= interactionDistance)
        {
            actionCursor.transform.position = obj.transform.position;
            actionCursor.SetActive(true);
        }
    }

    public void DisableCursor()
    {
        actionCursor.SetActive(false);
    }
}
