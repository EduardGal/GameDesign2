using UnityEngine;

public class CurrentRoomCanvas : Photon.MonoBehaviour {

    public GameObject loading;
    public GameObject StartMatch;
    public void OnClickStartSync()
    {
        if (!PhotonNetwork.isMasterClient)
            return;
        PhotonNetwork.LoadLevel(3);
    }


    public void OnClickStartDelayed()
    {
        this.gameObject.GetComponent<PhotonView>().photonView.RPC("StartGame", PhotonTargets.AllViaServer, null);

    }

    [PunRPC]
    public void StartGame()
    {


        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        loading.SetActive(true);
        //PhotonNetwork.LoadLevel(1);
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
