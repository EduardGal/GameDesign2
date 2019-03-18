using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class screenDim : Photon.MonoBehaviour


{

    public GameObject dimImage;
    // Start is called before the first frame update
    void Awake()
    {
        dimImage = GameObject.FindGameObjectWithTag("DimCanvas");
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
        this.GetComponent<PhotonView>().photonView.RPC("DimScreen", PhotonTargets.AllBufferedViaServer, null);
    }

    [PunRPC]
    IEnumerator DimScreen()
    {
        AsyncOperation sync;
        sync = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        sync.allowSceneActivation = false;


        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(1.5f);


        yield return new WaitForSeconds(3.5f);

        sync.allowSceneActivation = true;
        yield return new WaitForSeconds(1.5f);
        if (this.gameObject.tag == "PlayerOne")
        {
            gameObject.transform.position = new Vector3(41, 4.1f, 260);
            SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName("Framandi v1"));
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Framandi v1"));
            SceneManager.UnloadSceneAsync("KidsRoom");

        }
        else if(this.gameObject.tag == "PlayerTwo")
        {
            gameObject.transform.position = new Vector3(36, 4.4f, 226);
            SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName("Framandi"));
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Framandi v1"));
            SceneManager.UnloadSceneAsync("KidsRoom");
        }
        yield return null;

        this.gameObject.GetComponent<PlayerMovement>().enabled = true;
    }
}
