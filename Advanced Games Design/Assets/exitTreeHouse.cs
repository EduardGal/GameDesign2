using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exitTreeHouse : MonoBehaviour
{
    public Transform groundPos;
        public GameObject gameObject;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerOne" || other.tag == "PlayerTwo")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.transform.position = groundPos.position;
                FindObjectOfType<AudioManager>().Play("Climb");
            }
        }
    }
}