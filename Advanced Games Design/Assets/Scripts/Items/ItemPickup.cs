using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkReference;

public class ItemPickup : Interactables
{
    public float radius;
    public GameObject pickupPanel;
    private Transform playerOne, playerTwo;
    public Items item;
    private AnimationClip animationClip;

    private networkManager networkReference;

   

    public void Awake()
    {
        networkReference = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<networkManager>();
        animationClip = item.animation;

        var canvas = GameObject.Find("Canvas");
        Debug.Log("Found canvas" + canvas.name);
        pickupPanel = canvas.transform.Find("PickupPanel").gameObject;
    }

    public void Update()
    {
        if(playerOne == null && playerTwo== null)
        {
            playerOne = networkReference.playerOne.transform;
            //playerTwo = networkReference.playerTwo.transform;

        }
    }

    public void PickUpItem()
    {
        bool ItemPickedUp = Inventory.instance.AddItem(item);

        //Add item to inventory, remove from scene
        if (ItemPickedUp)
        {
            Destroy(gameObject);
            pickupPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerOne")
        {
            Debug.Log("Player one has entered");
            if (pickupPanel != null && Player.instance.itemInHand != this.gameObject)
            {
                pickupPanel.SetActive(true);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (pickupPanel != null)
        {
            pickupPanel.SetActive(false);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
