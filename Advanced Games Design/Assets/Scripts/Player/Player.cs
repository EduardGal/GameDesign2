using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public GameObject itemInHand;
    public GameObject playersHand;

    private void Awake()
    {
        StartCoroutine(PleaseWait());
        if (instance != null)
        {
            Debug.Log("Multiple players found");
        }
        instance = this;
    }

    public IEnumerator PleaseWait()
    {
        yield return new WaitForSeconds(1f);
        playersHand = GameObject.Find("ItemSpawnLocation");
    }
}
