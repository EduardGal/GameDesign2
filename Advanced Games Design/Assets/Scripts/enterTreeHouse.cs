using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterTreeHouse : MonoBehaviour
{

    public Transform treePos;
    public GameObject gameObject;

    public void DeactivateMe()
    {
        StartCoroutine(RemoveAfterSeconds(3));
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PlayerOne" || other.tag == "PlayerTwo")
        {
            if (Input.GetKeyDown(KeyCode.E)){
                other.gameObject.transform.position = treePos.position;
                gameObject.SetActive(true);

            }

        }
    }


    IEnumerator RemoveAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}