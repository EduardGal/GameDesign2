using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitTreeHouse : MonoBehaviour
{

    public Transform groundPos;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerOne" || other.tag == "PlayerTwo")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.transform.position = groundPos.position;
            }
        }
    }
}
