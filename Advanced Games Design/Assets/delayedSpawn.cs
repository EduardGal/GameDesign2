using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delayedSpawn : MonoBehaviour
{


    public GameObject[] drones;
    // Start is called before the first frame update 


    


    IEnumerator Start()
    {

        yield return new WaitForSeconds(6.5f);
       foreach(GameObject drone in drones)
        {
            drone.SetActive(true);
        }
        
    }
}