using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterTreeHouse : MonoBehaviour
{

    public Transform treePos;




    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PlayerOne" || other.tag == "PlayerTwo")
        {
            if (Input.GetKeyDown(KeyCode.E)){
                other.gameObject.transform.position = treePos.position;

            }

        }
    }
}