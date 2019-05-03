using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleStart : MonoBehaviour
{
    public GameObject puzzleCanvas;



    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PlayerOne" || other.tag == "PlayerTwo")
        {
            if (Input.GetKey(KeyCode.E))
            {
                puzzleCanvas.SetActive(true);
                other.GetComponent<Camera>().enabled = false;

            }
        }
    }
}
