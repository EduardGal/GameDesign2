using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class playerNetwork : MonoBehaviour {

    public static playerNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView photonView;
    public GameObject player, player2;
    public TextMeshProUGUI playernickname;
    public GameObject a, b, c;
    private int PlayersInGame = 0;
    public GameObject playerListing1, currentRoom;
    public bool hostIsPlayerOne;


    private void Awake()
    {
        Instance = this;
        photonView = GetComponent<PhotonView>();
        PlayerName = "Player " + Random.Range(01, 99);
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 60;
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "KidsRoom")
        {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();

            else NonMasterLoadedGame();
        }
    }

    private void MasterLoadedGame()
    {
        //PlayersInGame = 1;
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        Debug.Log("MasterLoadedGame");
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
        
    }

    private void NonMasterLoadedGame()
    {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        Debug.Log("Non Master Loaded Game");
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        //PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {

        PlayersInGame++;
        print(PlayersInGame);
        if (PlayersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players are in the game");

            photonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    }
    [PunRPC]
    private void RPC_CreatePlayer()
    {
        if (hostIsPlayerOne)
        {
            if (PhotonNetwork.player.IsMasterClient)
            {
                PhotonNetwork.Instantiate(player.name, new Vector3(-2, 0, 0), Quaternion.identity, 0);
            }
            if (!PhotonNetwork.player.IsMasterClient)
            {
                PhotonNetwork.Instantiate(player2.name, new Vector3(3, 0, 0), Quaternion.identity, 0);
            }

        }
        if (!hostIsPlayerOne)
        {
            if (PhotonNetwork.player.IsMasterClient)
            {
                PhotonNetwork.Instantiate(player2.name, new Vector3(-2, 0, 0), Quaternion.identity, 0);
            }
            if (!PhotonNetwork.player.IsMasterClient)
            {
                PhotonNetwork.Instantiate(player.name, new Vector3(3, 0, 0), Quaternion.identity, 0);
            }

        }


    }

    public void OnCreateUsername()
    {

        PhotonNetwork.player.NickName = playernickname.text;
        a.SetActive(true);
        b.SetActive(true);
        c.SetActive(true);
        playernickname.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);

    }

    
    public void OnChangeCharacterClick()
    {
        photonView.RPC("ChangeCharacter", PhotonTargets.AllViaServer);
    }

    [PunRPC]
    public void ChangeCharacter()
    {
        playerListing1.transform.GetChild(0).GetComponent<switchCharacter>().ChangeCharacter();
        playerListing1.transform.GetChild(1).GetComponent<switchCharacter>().ChangeCharacter();

    }


    public void OnKick()
    {
        photonView.RPC("OnKicked", PhotonTargets.Others);
    }
    [PunRPC]
    public void OnKicked()
    {
        if (!PhotonNetwork.player.IsMasterClient)
        {
            currentRoom.SetActive(false);
        }
    }

    public void ChangeTags()
    {
        photonView.RPC("M_ChangeTags", PhotonTargets.AllBufferedViaServer);
            
    }

    [PunRPC]

    public void M_ChangeTags()
    {
        player.tag = "PlayerOne";
        player2.tag = "PlayerTwo";
    }

}
