using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerLoadScene : MonoBehaviour
{

    public GameObject enterText;
    public string levelToLoad;
    public GameObject networkManager;

    void Start()
    {
        enterText.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerStay(Collider plyr)
    {
        if (plyr.gameObject.tag == "PlayerOne" || plyr.gameObject.tag == "PlayerTwo")
        {
            networkManager.SetActive(true);
            enterText.SetActive(true);
            if (Input.GetKeyUp(KeyCode.E))
            {

                networkManager.gameObject.GetComponent<screenDim>().ChangeToFramandi();
                enterText.SetActive(false);
                // GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<screenDim>().ChangeToFramandi();
                // GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<screenDim>().ChangeToFramandi();
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(this);
            }
        }
    }
    void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.tag == "PlayerOne" || plyr.gameObject.tag == "PlayerTwo")
        {
            enterText.SetActive(false);
        }
    }



}