using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ifnoservers : MonoBehaviour
{
    public GameObject serverChecker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(serverChecker.transform.childCount == 0)
        {
            this.GetComponent<TextMeshProUGUI>().text = ("There are currently 0 open servers");
        }else this.GetComponent<TextMeshProUGUI>().text = ("");
    }
}
