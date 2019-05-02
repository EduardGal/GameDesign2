using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentRoomCanvas : Photon.MonoBehaviour {

    public GameObject loading;
    public GameObject StartMatch;
    public void OnClickStartSync()
    {
        if (!PhotonNetwork.isMasterClient)
            return;
        PhotonNetwork.LoadLevel(1);
    }


    public void OnClickStartDelayed()
    {
        this.gameObject.GetComponent<PhotonView>().photonView.RPC("StartGame", PhotonTargets.AllViaServer, null);

    }

    [PunRPC]
    public void StartGame()
    {
        
       // SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        loading.SetActive(true);
        AsyncOperation op = PhotonNetwork.LoadLevelAsync(1);


    }

    public void Update()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            StartMatch.gameObject.SetActive(true);
        }
        else StartMatch.gameObject.SetActive(false);
    }

}
