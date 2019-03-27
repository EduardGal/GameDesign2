using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ifNotHost : Photon.MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            this.gameObject.SetActive(true);
        }
        else this.gameObject.SetActive(false);
    }
}
