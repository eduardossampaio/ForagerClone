using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 1;
    public float currentEnergy = 5;
    public float maxEnergy = 15;

    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private Inventory inventory;

    private Vector2 movementInput;
    private Vector2 mousePosition;

    private bool walking = false;
    private bool lookingLeft = false;

 
    private bool isAction = false;

    private bool isActionButtonPressed;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x < transform.position.x && !lookingLeft)
        {
            Flip();
        }else if(mousePosition.x > transform.position.x && lookingLeft)
        {
            Flip();
        }

        if (Input.GetButtonDown("Fire1") && !isAction)
        {
            isActionButtonPressed = true;              
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isActionButtonPressed = false;
        }

        if (isActionButtonPressed && !isAction)
        {
            isAction = true;
            animator.SetTrigger("axe");
        }

        if(Input.GetButtonDown("Cancel"))
        {
            CoreGame.instance.ShowInventory(inventory);
        }

        movementInput = Vector2.zero;
        if (!isAction)
        {
            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        walking = movementInput.magnitude != 0;

        rigidBody2D.velocity = movementInput * movementSpeed;

        animator.SetBool("walking", walking);


    }

    private void Flip()
    {
        if (isAction)
        {
            return;
        }
        lookingLeft = !lookingLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, 1, 1);
    }
    public void AxeHit()
    {
        CoreGame.instance.gameManager.PerformHit();
        print("HIT");
    }
    private void ActionDone()
    {
        isAction = false;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Loot":
                var loot = collision.GetComponent<Loot>();
                if (loot != null)
                {
                    inventory.AddItem(loot.item, 1);
                    Destroy(collision.gameObject);
                }
                break;
        }
    }

    public void AddEnergy(int amount)
    {
        currentEnergy += amount;
        if(currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }

    public void UseItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.FOOD:
                AddEnergy(item.energyAmount);
                break;
        }
    }
}
