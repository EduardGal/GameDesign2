using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class screenDim : Photon.MonoBehaviour


{
    AsyncOperation sync;

    public GameObject dimImage;
    // Start is called before the first frame update
    void Awake()
    {
        dimImage = GameObject.FindGameObjectWithTag("DimCanvas");


        sync.allowSceneActivation = false;
       // this.GetComponent<PhotonView>(). photonView.RPC("DimScreen", PhotonTargets.AllBufferedViaServer, null);
    }

    // Update is called once per frame
    void Update()
    {
        //DontDestroyOnLoad(this.gameObject);
        //dimImage.GetComponent<Image>().color = new Color(0, 0, 0, Mathf.SmoothStep(255, 0, 2 * Time.deltaTime));
    }

    public void ChangeToFramandi()
    {
            
            this.GetComponent<PhotonView>().photonView.RPC("DimScreen", PhotonTargets.AllViaServer, null);


    }

    [PunRPC]
    IEnumerator DimScreen()
    {



        sync = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(3.5f);


       // yield return new WaitForSeconds(3.5f);

        sync.allowSceneActivation = true;
       yield return new WaitForSeconds(.1f);


       if (this.gameObject.tag == "PlayerOne")
        {
            gameObject.transform.position = new Vector3(41, 5f, 260);
            SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName("Framandi v1"));
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));



        }
        if(this.gameObject.tag == "PlayerTwo")
       {
            gameObject.transform.position = new Vector3(36, 5f, 226);
            SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName("Framandi v1"));
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));

        }
        yield return new WaitForSeconds(.2f);


        Debug.Log("SetScene");
        this.gameObject.GetComponent<PlayerMovement>().enabled = true;
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
