using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class delayedSpawn : MonoBehaviour
{


    public GameObject[] drones;
    public GameObject[] NetworkScripts;
    public GameObject[] narrative;

    // Start is called before the first frame update 


    public void Update()
    {
        if(SceneManager.GetActiveScene().name == "Framandi v1")
        {
            StartCoroutine(Delay());
        }
    }


    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(.3f);

        foreach(GameObject nar in narrative)
        {
            nar.SetActive(true);
        }
        
        //Debug.Log("start Delay");
       yield return new WaitForSeconds(3.5f);

        foreach (GameObject network in NetworkScripts)
        {
            network.SetActive(true);
        }
        yield return new WaitForSeconds(1.5f);
        foreach (GameObject drone in drones)
        {
            drone.SetActive(true);
        }
        Destroy(this);
       
    }
}