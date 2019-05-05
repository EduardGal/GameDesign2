using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.GlobalIllumination;

public class syncColor : Photon.MonoBehaviour
{
    public Light spotlight;
    public void Awake()
    {
        spotlight = GetComponent<Light>();
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Vector3 tempCol;
        if (stream.isWriting)
        {
            tempCol = new Vector3(spotlight.color.r, spotlight.color.g, spotlight.color.b);
            stream.SendNext(tempCol);


        }
        else
        {
            tempCol = (Vector3)stream.ReceiveNext();
            spotlight.color = new Color(tempCol.x, tempCol.y, tempCol.z, 1f);

        }
    }
}
