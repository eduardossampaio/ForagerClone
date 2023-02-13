using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject actionCursor;
    public float interactionDistance;

    private GameObject interactingObject;
    private float distanceToObject;

    public float timeToDeleteItem = 3;
    public void ActiveCursor(GameObject obj)
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

    public void PerformHit()
    {
        if(interactingObject == null || !interactingObject.activeSelf)
        {
            return;
        }
        if (distanceToObject >= interactionDistance)
        {
            return;
        }
        interactingObject.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
    }

    private void FixedUpdate()
    {
        if(interactingObject == null)
        {
            return;
        }

        distanceToObject = Vector2.Distance(CoreGame.instance.player.transform.position, interactingObject.transform.position);

        if (distanceToObject <= interactionDistance)
        {
            ActiveCursor(interactingObject);
        }
        else
        {
            DisableCursor();
        }
    }

  
}
