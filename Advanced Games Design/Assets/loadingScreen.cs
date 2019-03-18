using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadingScreen : Photon.MonoBehaviour

{
    AsyncOperation async, scene;
    List<AsyncOperation> allScenes = new List<AsyncOperation>();
    const int sceneMax = 3;
    bool doneLoadingScenes = false;
    public bool isLoaded;
    public Image loading;
    public Text loadingText;
    // Start is called before the first frame update
    void Awake()
    {
        //loading.transform.localScale = new Vector3(0, 0, 0);

    }

    private void Update()
    {



    }

}
