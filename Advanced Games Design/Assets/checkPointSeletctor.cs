using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkPointSeletctor : MonoBehaviour
{
    public Dropdown.OptionData checkpoint;

    private void Awake()
    {
       if( PlayerPrefs.GetInt("Checkpoint1") == 1)
        {
            GetComponent<Dropdown>().options.Add(checkpoint);
        }
    }


}
