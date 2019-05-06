using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dim : Photon.MonoBehaviour
{
    GameObject playerone;
    GameObject playertwo;
    private void Update()
    {
        if (playerone == null || playertwo == null){
            playerone = GameObject.FindGameObjectWithTag("PlayerOne");
            playertwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        }

        if (playerone != null && playertwo !=null){
            photonView.RPC("FadeTo", PhotonTargets.AllBufferedViaServer, 0, 3);
        }
    }

    [PunRPC]
    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }
}
