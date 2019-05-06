﻿using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class playerNetwork : MonoBehaviour {

    public static playerNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView photonView;
    public GameObject player;
    public TextMeshProUGUI playernickname;
    public GameObject a, b, c;
    private int PlayersInGame = 0;
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
        Debug.Log("MasterLoadedGame");
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }

    private void NonMasterLoadedGame()
    {
        Debug.Log("Non Master Loaded Game");
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
     //   PhotonNetwork.LoadLevel(1);
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
        PhotonNetwork.Instantiate(player.name, new Vector3(-2,0,0), Quaternion.identity, 0);
        
    }

    public void OnCreateUsername()
    {

        PhotonNetwork.player.NickName = playernickname.text;
        a.SetActive(true);
        b.SetActive(true);
        c.SetActive(true);
        playernickname.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);

    }

}
