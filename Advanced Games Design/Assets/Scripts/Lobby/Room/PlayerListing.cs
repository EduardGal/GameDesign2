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
        PlayerNetwork.GetComponent<playerNetwork>().playerListing = this.gameObject;
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

    


    [PunRPC]
    public void ChangeCharacter()
    {

        if (PhotonNetwork.player.IsMasterClient)
        {
            if (masterIsSisterOne)
            {
                sisterOne.SetActive(false);
                sisterTwo.SetActive(true);
                masterToTwo = true;
            }
                
            }
            if(!masterIsSisterOne)
            {
                sisterOne.SetActive(true);
                sisterTwo.SetActive(false);
                masterToTwo = false;
               
            }

        if (!PhotonNetwork.player.IsMasterClient)
        {
            if (clientIsSisterTwo)
            {
                sisterOne.SetActive(true);
                sisterTwo.SetActive(false);
                masterToTwo = false;
            }
            if (!clientIsSisterTwo)
            {
                sisterOne.SetActive(false);
                sisterTwo.SetActive(true);
                masterToTwo = true;
            }

        }
        

        if(masterToTwo == true)
        {
            clientIsSisterTwo = false;
            masterIsSisterOne = false;
            masterToTwo = false;
        }
        else
        {
            clientIsSisterTwo = true;
            masterIsSisterOne = true;
            
        }
    }
        
    


    
}
