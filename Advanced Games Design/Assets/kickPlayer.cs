using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickPlayer : Photon.MonoBehaviour
{
    // Start is called before the first frame update
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

       
    }

}
