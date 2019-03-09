using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class photonHandler : MonoBehaviour {

    public photonButtons photonB;

    public GameObject mainPlayer;

    public Vector3[] spawnPoint;

    private void Awake()
    {
        DontDestroyOnLoad(this.transform);
        spawnPoint[1] = new Vector3(-2, 0, 0);
        spawnPoint[2] = new Vector3(0, 0, 0);
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 60;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }


    public void moveScene()
    {
        PhotonNetwork.LoadLevel("KidsRoom");
    }

    public void CreateNewRoom()
    {
        PhotonNetwork.CreateRoom(photonB.createRoomInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
    }


    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "KidsRoom")
        {
            spawnPlayer();
        }

    }

    private void spawnPlayer()
    {

        int playerNum =1;

        if (playerNum == 1)
        {
            PhotonNetwork.Instantiate(mainPlayer.name, spawnPoint[1], mainPlayer.transform.rotation, 0);
            playerNum = 2;
        }else if (playerNum == 2)
            {
                PhotonNetwork.Instantiate(mainPlayer.name, spawnPoint[2], mainPlayer.transform.rotation, 0);
                playerNum = 3;
            }
    }
}
