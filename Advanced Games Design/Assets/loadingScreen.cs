using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadingScreen : Photon.MonoBehaviour
    
{
    AsyncOperation async;
    public bool isLoaded;
    public Image loading;
    public Text loadingText;
    // Start is called before the first frame update
    void Awake()
    {
    PhotonNetwork.LoadLevelAsync(3);
    
        async.allowSceneActivation = false;
        loading.transform.localScale = new Vector3(0.1f, -.5f, 0.3f);
        this.gameObject.GetComponent<PhotonView>().RPC("LoadLevel", PhotonTargets.AllViaServer, null);
    }

    [PunRPC]
    void LoadLevel()
    {
        SceneManager.LoadScene("Framandi v1", LoadSceneMode.Additive);
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {

        isLoaded = PhotonNetwork.LoadLevelAsync(3).isDone;
        float progress = PhotonNetwork.LoadLevelAsync(3).progress;
        loadingText.text = PhotonNetwork.LoadLevelAsync(3).progress.ToString() +  " %";
        loading.transform.localScale = new Vector3(progress / 50, -.5f, 0.3f);
    }
}
