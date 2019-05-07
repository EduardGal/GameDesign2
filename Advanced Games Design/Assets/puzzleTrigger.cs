using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class puzzleTrigger : MonoBehaviour
{

    public GameObject puzzleCanvas;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerOne" || other.tag == "PlayerTwo")
        {
            other.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "Press E to open chest";
            if (Input.GetKeyDown(KeyCode.E))
            {
                puzzleCanvas.SetActive(true);
                other.gameObject.SetActive(false);
                GetComponent<PhotonView>().RPC("OnPuzzleStart", PhotonTargets.AllViaServer);
                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "";
    }

    [PunRPC]
    void OnPuzzleStart()
    {
        puzzleCanvas.SetActive(true);
        
    }
}
