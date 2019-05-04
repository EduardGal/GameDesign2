using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaveTriggerColllider : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PuzzlePlayer")
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
