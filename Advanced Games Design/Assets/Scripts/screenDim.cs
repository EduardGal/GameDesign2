using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class screenDim : Photon.MonoBehaviour


{

    AsyncOperation sync;

    public GameObject dimImage;
    private GameObject playerOne, playerTwo;
    public List<GameObject> playersInGame;

    // Start is called before the first frame update
    void Awake()
    {
        //DontDestroyOnLoad(this);
        dimImage = GameObject.FindGameObjectWithTag("DimCanvas");
        playersInGame.Capacity = 2;


       // this.GetComponent<PhotonView>(). photonView.RPC("DimScreen", PhotonTargets.AllBufferedViaServer, null);
    }

    // Update is called once per frame
    void Update()
    {
        //DontDestroyOnLoad(this.gameObject);
        //dimImage.GetComponent<Image>().color = new Color(0, 0, 0, Mathf.SmoothStep(255, 0, 2 * Time.deltaTime));
        if (playerOne == null)
        {
            playerOne = GameObject.FindGameObjectWithTag("PlayerOne");
            playersInGame.Add(playerOne);
        }

        if (playerTwo == null)
        {
            playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
            playersInGame.Add(playerTwo);
        }



    }       
        
    

    public void ChangeToFramandi()
    {
            
            this.GetComponent<PhotonView>().photonView.RPC("DimScreen", PhotonTargets.AllViaServer, null);


    }

    [PunRPC]
    public IEnumerator DimScreen()
    {

        yield return new WaitForSeconds(0.2f);


        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        yield return new WaitForSeconds(.1f);



            foreach(GameObject players in playersInGame)
            {
                players.transform.position = new Vector3(41, 5f, 260);
                SceneManager.MoveGameObjectToScene(players, SceneManager.GetSceneByName("Framandi v1"));
            }
          


        
        yield return new WaitForSeconds(.1f);

        
        Debug.Log("SetScene");
        //this.gameObject.GetComponent<PlayerMovement>().enabled = true;
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            print(scene.name);
            if (scene.name != "Framandi v1")
            {
                SceneManager.UnloadSceneAsync(scene);
                Debug.Log("Unloaded");
            }
        }
    }
}
