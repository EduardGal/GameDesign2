using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class exitTreeHouse : MonoBehaviour
{

    public Transform groundPos;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerOne" || other.tag == "PlayerTwo")
        {
            other.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "Press E to exit tree house";
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.transform.position = groundPos.position;
                FindObjectOfType<AudioManager>().Play("Climb");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "";
    }

}