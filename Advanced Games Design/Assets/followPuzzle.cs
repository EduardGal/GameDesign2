using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPuzzle : MonoBehaviour
{
    public GameObject player;
    

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
    }
}
