using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayer : MonoBehaviour
{
    public float offset;
    private SpriteRenderer thisSpriteRenderer;
    private float playerY;
    void Start()
    {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if(thisSpriteRenderer == null) { return; }

       playerY = CoreGame.instance.player.PositionY;

        if(transform.position.y < playerY - offset)
        {
            thisSpriteRenderer.sortingLayerName = "Primeiro_Plano";
        }
        else
        {
            thisSpriteRenderer.sortingLayerName = "Segundo_Plano";
        }
    }
}
