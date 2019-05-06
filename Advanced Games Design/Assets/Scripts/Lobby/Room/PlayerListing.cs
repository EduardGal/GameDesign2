using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour {

    public PhotonPlayer PhotonPlayer { get; private set; }
    public GameObject sisterOne, sisterTwo;
    public GameObject master, client;
    public bool masterIsSisterOne, clientIsSisterTwo, masterToTwo;
    public GameObject PlayerNetwork;



    [SerializeField]
    private Text _PlayerName;
    private Text PlayerName    
    {
        get { return _PlayerName; }
    }


    public void ApplyPhotonPlayer(PhotonPlayer photonPlayer)
    {
        PlayerNetwork = GameObject.FindGameObjectWithTag("PlayerNetwork");
        if (PhotonNetwork.player.IsMasterClient) PlayerNetwork.GetComponent<playerNetwork>().playerListing1 = this.gameObject;
        if (!PhotonNetwork.player.IsMasterClient) PlayerNetwork.GetComponent<playerNetwork>().playerListing2 = this.gameObject;
        PhotonPlayer = photonPlayer;
        PlayerName.text = photonPlayer.NickName;
        if (photonPlayer.IsMasterClient)
        {
            
            sisterOne.SetActive(true);
            masterIsSisterOne = true;
        }
        else
        {
            
            sisterTwo.SetActive(true);
            clientIsSisterTwo = true;
        }
    }

    



        
    


    
}
