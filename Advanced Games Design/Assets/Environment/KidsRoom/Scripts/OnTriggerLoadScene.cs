using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerLoadScene : MonoBehaviour
{

    public GameObject enterText;
    public string levelToLoad;
    private GameObject PlayerOne, PlayerTwo;


    void Start()
    {
        enterText.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerStay(Collider plyr)
    {
        if (plyr.gameObject.tag == "PlayerOne" || plyr.gameObject.tag == "PlayerTwo")
        {
            enterText.SetActive(true);
            if (Input.GetButtonDown("Use"))
            {
                this.gameObject.GetComponent<PhotonView>().photonView.RPC("LoadLevelToFramandi", PhotonTargets.AllViaServer, null);
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

    [PunRPC]
    void LoadLevelToFramandi()
    {
        PlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        PlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");

        SceneManager.MoveGameObjectToScene(PlayerOne, SceneManager.GetSceneByBuildIndex(1));
        PlayerOne.transform.position = new Vector3(40, 4.8f, 260);
        SceneManager.MoveGameObjectToScene(PlayerTwo, SceneManager.GetSceneByBuildIndex(1));
        PlayerTwo.transform.position = new Vector3(33, 4.8f, 255);

        SceneManager.UnloadSceneAsync(3);
    }
}