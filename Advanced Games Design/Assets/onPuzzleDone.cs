using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPuzzleDone : MonoBehaviour
{
    int playerCount;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "PuzzlePlayer") playerCount++;
        if (other.tag == "PuzzlePlayer2") playerCount++;

       
    }

    public void Update()
    {
        if(playerCount == 2)
        {
            Debug.LogWarning("Puzzle Done");
            transform.parent.gameObject.SetActive(true);
        }
    }
}
