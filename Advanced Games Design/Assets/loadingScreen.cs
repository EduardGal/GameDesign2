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
        PhotonNetwork.LoadLevelAsync(1);
        
        loading.transform.localScale = new Vector3(0.1f, -.5f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        async.allowSceneActivation = false;
        isLoaded = PhotonNetwork.LoadLevelAsync(1).isDone;
        float progress = PhotonNetwork.LoadLevelAsync(1).progress;
        loadingText.text = PhotonNetwork.LoadLevelAsync(1).progress.ToString() +  " %";
        loading.transform.localScale = new Vector3(progress / 50, -.5f, 0.3f);
    }
}
