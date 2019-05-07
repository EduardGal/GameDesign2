﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enterTreeHouse : MonoBehaviour
{

    public Transform treePos;
    private GameObject network;


    private void OnTriggerStay(Collider other)
    {
       
        if(other.tag == "PlayerOne" || other.tag == "PlayerTwo")
        {
            other.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "Press E to enter tree house";
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.transform.position = treePos.position;
                FindObjectOfType<AudioManager>().Play("Climb");
                network.GetComponent<playerNetwork>().Checkpoint1();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerOne" || other.tag == "PlayerTwo")
        {
            other.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "";

        }
    }
}