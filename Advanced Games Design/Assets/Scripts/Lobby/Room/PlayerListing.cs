using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour {

    public PhotonPlayer PhotonPlayer { get; private set; }
    public GameObject sisterOne, sisterTwo;


    [SerializeField]
    private Text _PlayerName;
    private Text PlayerName    
    {
        get { return _PlayerName; }
    }


    public void ApplyPhotonPlayer(PhotonPlayer photonPlayer)
    {
        PhotonPlayer = photonPlayer;
        PlayerName.text = photonPlayer.NickName;
        if (photonPlayer.IsMasterClient)
        {
            sisterOne.SetActive(true);
        }
        else
        {
            sisterTwo.SetActive(true);
        }
    }
}
