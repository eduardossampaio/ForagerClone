using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    public Item item;
    private float startYPosition;
    private Rigidbody2D rigidBody;
    private bool isActive;
    private Collider2D thisCollider;
    
    public void Active(int dir)
    {
        rigidBody = GetComponent<Rigidbody2D>();
        thisCollider = GetComponent<Collider2D>();
        
        startYPosition = transform.position.y;
        rigidBody.gravityScale = 1.8f;
        rigidBody.AddForce(Vector2.up * 250 + Vector2.right * (Random.Range(20, 35) * dir));
        isActive = true;
        thisCollider.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (isActive && transform.position.y < startYPosition - Random.Range(0.2f,0.6f))
        {
            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
            isActive = false;
            thisCollider.enabled = true;
        }
    }

  
}
