using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


    private void OnApplicationQuit()
    {
        PhotonNetwork.Disconnect();
    }

    private void OnDestroy()
    {
        PhotonNetwork.Disconnect();
    }

}
