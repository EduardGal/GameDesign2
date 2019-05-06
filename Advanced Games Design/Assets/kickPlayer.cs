using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickPlayer : Photon.MonoBehaviour

{

    public GameObject currentRoom;
    public GameObject network;
    // Start is called before the first frame update

    private void Start()
    {
        network = GameObject.FindGameObjectWithTag("PlayerNetwork");
    }
    void Update()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else transform.GetChild(0).gameObject.SetActive(false);
    }


    public void OnKickPlayer()
    {     
        PhotonNetwork.CloseConnection(PhotonPlayer.Find(2));
        network.GetComponent<playerNetwork>().OnKick();
    }

}
