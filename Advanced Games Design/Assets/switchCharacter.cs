using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCharacter : MonoBehaviour
{

    public GameObject playerOnePref, playerTwoPref, newtork;
    public bool needChange;


    private void Awake()
    {
        playerOnePref = gameObject.transform.GetChild(0).gameObject;
        playerTwoPref = gameObject.transform.GetChild(1).gameObject;
    }

    public void ChangeCharacter()
    {
        newtork = GameObject.FindGameObjectWithTag("PlayerNetwork");



        
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
