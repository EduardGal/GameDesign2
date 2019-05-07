using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OnTriggerLoadScene : MonoBehaviour
{

    public string levelToLoad;
    public GameObject networkManager;

    void Start()
    {
       
    }

    // Update is called once per frame
    void OnTriggerStay(Collider plyr)
    {
        if (plyr.gameObject.tag == "PlayerOne" || plyr.gameObject.tag == "PlayerTwo")
        {
            plyr.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "Press E to leave room";
            networkManager.SetActive(true);
            
            if (Input.GetKeyUp(KeyCode.E))
            {

                networkManager.gameObject.GetComponent<screenDim>().ChangeToFramandi();
                plyr.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "";
                // GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<screenDim>().ChangeToFramandi();
                // GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<screenDim>().ChangeToFramandi();
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(this);
                FindObjectOfType<AudioManager>().Play("SoundtrackGame");
            }
        }
    }
    void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.tag == "PlayerOne" || plyr.gameObject.tag == "PlayerTwo")
        {
            plyr.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }



}