using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class initiateMenu : MonoBehaviour {

    public photonConnect phConnect;
    public GameObject MainMenu;


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update () {
        if (Input.anyKey)
        {
            phConnect.StartConnection();
            MainMenu.SetActive(true);
            this.gameObject.SetActive(false);

        }
	}

   


}
