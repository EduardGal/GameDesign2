﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class delayedSpawn : MonoBehaviour
{


    public GameObject[] drones;
    // Start is called before the first frame update 


    public void Update()
    {
        if (SceneManager.sceneCount == 2)
        {

        }
        else Delay();
    }


    IEnumerator Delay()
    {

        yield return new WaitForSeconds(6.5f);
       foreach(GameObject drone in drones)
        {
            drone.SetActive(true);
        }
        
    }
}