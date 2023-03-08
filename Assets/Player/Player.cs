using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventSystem;

public class Player : MonoBehaviour
{
    public class PlayerEvents
    {
        public static string PLAYER_MOVED = "PlayerEvents.PLAYER_MOVED";
        public static string PLAYER_HIT = "PlayerEvents.PLAYER_HIT";
    }

    public GameConfigurations configurations;

    public int level = 1;
    public float movementSpeed = 1;
    public float currentEnergy = 5;
    public float maxEnergy = 15;
    public float PositionY
    {
        get
        {
            return transform.position.y;
        }
    }

    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private Inventory inventory;

    private Vector2 movementInput;
    private Vector2 mousePosition;

    private bool walking = false;
    private bool lookingLeft = false;

 
    private bool isAction = false;

    private NotificationAgent notificationAgent;

    private bool isActionButtonPressed;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();

        RegisterNotifications();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            CoreGame.instance.ShowInventory(inventory);
        }

        if (configurations.gameState != GameState.GAMEPLAY)
        {
            return;
        }
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


        movementInput = Vector2.zero;
        if (!isAction)
        {
            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        walking = movementInput.magnitude != 0;

        rigidBody2D.velocity = movementInput * movementSpeed;

        animator.SetBool("walking", walking);
        if (walking)
        {
            notificationAgent.NotifyEvent(PlayerEvents.PLAYER_MOVED, this);
        }

    }


    private void RegisterNotifications()
    {
        notificationAgent = GetComponent<NotificationAgent>();
        notificationAgent.RegisterEvent<Item>(ItemEvents.USE_ITEM, UseItem);
        notificationAgent.RegisterEvent<Item>(ItemEvents.DELETE_ITEM, DeleteItem);
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
        //CoreGame.instance.gameManager.PerformHit();
        print("HIT");
        notificationAgent.NotifyEvent(PlayerEvents.PLAYER_HIT,this);
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
        print("using item");
        if (item.category == ItemCategory.CONSUMABLE)
        {
            inventory.UseItem(item);
            if (item.recoveryEnergy)
            {
                AddEnergy(item.energyAmount);
            }
        }
       
    }

    public void DeleteItem(Item item)
    {
        inventory.DeleteItem(item);
    }
}
