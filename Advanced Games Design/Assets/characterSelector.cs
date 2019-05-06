using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSelector : MonoBehaviour
{


    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("PlayerNetwork").GetComponent<playerNetwork>().hostIsPlayerOne)
        {
            if (PhotonNetwork.player.IsMasterClient)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(transform.GetChild(1).gameObject);
            }
            if (!PhotonNetwork.player.IsMasterClient)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                Destroy(transform.GetChild(0).gameObject);
            }
        }
        if (!GameObject.FindGameObjectWithTag("PlayerNetwork").GetComponent<playerNetwork>().hostIsPlayerOne)
        {
            if (PhotonNetwork.player.IsMasterClient)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                Destroy(transform.GetChild(0).gameObject);
            }
            if (!PhotonNetwork.player.IsMasterClient)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(transform.GetChild(1).gameObject);
            }
        }
    }

}
