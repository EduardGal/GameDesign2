﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCharacter : MonoBehaviour
{

    public GameObject playerOnePref, playerTwoPref, newtork;
    public bool needChange;




    public void ChangeCharacter()
    {
        newtork = GameObject.FindGameObjectWithTag("PlayerNetwork");

        if(PhotonNetwork.player.IsMasterClient) newtork.GetComponent<playerNetwork>().playerListing1 = gameObject.transform.parent.GetChild(0).gameObject;
        if (!PhotonNetwork.player.IsMasterClient) newtork.GetComponent<playerNetwork>().playerListing2 = gameObject.transform.parent.GetChild(1).gameObject;

       // playerOnePref = gameObject.transform.GetChild(0).gameObject;
       // playerTwoPref = gameObject.transform.GetChild(1).gameObject;
        needChange = true;

        if (needChange == true)
        {
            if (playerOnePref.activeInHierarchy == true)
            {
                playerTwoPref.SetActive(true);
                playerOnePref.SetActive(false);
                needChange = false;
            }
        }
        if (needChange == true)
        {
            if (playerTwoPref.activeInHierarchy == true)
            {
                playerOnePref.SetActive(true);
                playerTwoPref.SetActive(false);
                needChange = false;
            }
        }
    }
}
