using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class screenDim : Photon.MonoBehaviour


{
    public CanvasGroup uiElement;
    AsyncOperation sync;
    public GameObject narSystem;
    public GameObject dimImage;
    private GameObject playerOne, playerTwo;
    public List<GameObject> playersInGame;


    void Awake()
    {

        dimImage = GameObject.FindGameObjectWithTag("DimCanvas");
        playersInGame.Capacity = 2;



    }

    public bool devTesting;
    void Update()
    {
        if(devTesting) StartCoroutine(StartGame(uiElement, uiElement.alpha, 0));

        if (playerOne == null || playerTwo == null)
        {
            if (playerOne == null)
            {
                playerOne = GameObject.FindGameObjectWithTag("PlayerOne");
                if (playerOne != null)
                {
                    playersInGame.Add(playerOne);
                }
            }

            if (playerTwo == null)
            {
                playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
                if (playerTwo != null)
                {
                    playersInGame.Add(playerTwo);
                }
            }
        }
        StartCoroutine(StartGame(uiElement, uiElement.alpha, 0));


    }       
        
    IEnumerator StartGame(CanvasGroup cg, float start, float end, float lerpTime = .5f)
    {
        Debug.LogWarning("StartingGame");

       

        while (true)
        {
         

            float currentValue = Mathf.Lerp(start, end, 1*Time.deltaTime);

            cg.alpha = currentValue;

            if (cg.alpha == 0) break;
            yield return new WaitForEndOfFrame();
        }

        if (GameObject.FindGameObjectWithTag("PlayerNetwork").GetComponent<playerNetwork>().startAtCheckpoint)
        {
            this.GetComponent<PhotonView>().photonView.RPC("DimScreenCheckPoint", PhotonTargets.AllViaServer, null);
        }
        



    }

    public void ChangeToFramandi()
    {
            
            this.GetComponent<PhotonView>().photonView.RPC("DimScreen", PhotonTargets.AllViaServer, null);
        Debug.LogWarning("Changing Scene");

    }

    [PunRPC]
    public IEnumerator DimScreenCheckPoint()
    {

        yield return new WaitForSeconds(0.2f);


        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        yield return new WaitForSeconds(.1f);



        foreach (GameObject players in playersInGame)
        {
            players.transform.position = new Vector3(127, 2.9f, 155);
            SceneManager.MoveGameObjectToScene(players, SceneManager.GetSceneByName("Framandi v1"));
        }




        yield return new WaitForSeconds(.1f);


        Debug.LogWarning("SetScene");
        //this.gameObject.GetComponent<PlayerMovement>().enabled = true;
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            print(scene.name);
            if (scene.name != "Framandi v1")
            {
                SceneManager.UnloadSceneAsync(scene);
                Debug.LogWarning("Unloaded");
            }
        }
    }

    [PunRPC]
    public IEnumerator DimScreen()
    {

        yield return new WaitForSeconds(0.2f);


        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        yield return new WaitForSeconds(.1f);



            foreach(GameObject players in playersInGame)
            {
            players.transform.position = new Vector3(Random.RandomRange(36, 42), 5f, 260);
            SceneManager.MoveGameObjectToScene(players, SceneManager.GetSceneByName("Framandi v1"));
            }
          


        
        yield return new WaitForSeconds(.1f);

        
        Debug.LogWarning("SetScene");
        //this.gameObject.GetComponent<PlayerMovement>().enabled = true;
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            print(scene.name);
            if (scene.name != "Framandi v1")
            {
                SceneManager.UnloadSceneAsync(scene);
                Debug.LogWarning("Unloaded");
            }
        }
    }
}
