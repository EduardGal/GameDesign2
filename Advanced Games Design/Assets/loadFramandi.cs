using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadFramandi : MonoBehaviour
{



    [PunRPC]
    public void LoadToFramandi()
    {

        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        
    }
}
