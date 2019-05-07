using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class screenDim : Photon.MonoBehaviour


{

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


    void Update()
    {
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
        StartCoroutine(StartGame(0, 3));


    }       
        
    IEnumerator StartGame(float aValue, float aTime)
    {
        float alpha = dimImage.GetComponent<RawImage>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            dimImage.GetComponent<RawImage>().color = newColor;
            narSystem.SetActive(true);
            yield return null;
        }
    }

    public void ChangeToFramandi()
    {
            
            this.GetComponent<PhotonView>().photonView.RPC("DimScreen", PhotonTargets.AllViaServer, null);
        Debug.LogWarning("Changing Scene");

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
