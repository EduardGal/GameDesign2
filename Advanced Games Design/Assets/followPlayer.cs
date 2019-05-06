using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("PlayerOne");

        }

        if(player != null)
        {
            this.transform.position = player.transform.position;
        }
    }
}
