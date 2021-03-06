﻿using UnityEngine;

public class InteractableManager : MonoBehaviour {

    [SerializeField] private float radius;
    Collider collider;
    
    private float distance;
    private bool trigger;
    private bool playerCanPickup = false;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(trigger && collider != null)
        {
            distance = Vector3.Distance(transform.position, collider.transform.position);

            if (collider.gameObject.tag == "Interactable" && distance <= radius)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //collider.gameObject.GetComponent<ItemPickup>().PickUpItem();
                    playerCanPickup = true;
                    PickupItemAnimation();
                }
            }
        }
        else
        {
            playerCanPickup = false;
        }

        //Debug.Log(playerCanPickup);
    }

    public void PickupItemAnimation()
    {
        if (playerCanPickup)
        {
            animator.SetBool("pickupItem", true);
            collider.gameObject.GetComponent<ItemPickup>().PickUpItem();
        }

        playerCanPickup = false;
        animator.SetBool("pickupItem", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        collider = other;
        trigger = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        collider = null;
        trigger = false;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
