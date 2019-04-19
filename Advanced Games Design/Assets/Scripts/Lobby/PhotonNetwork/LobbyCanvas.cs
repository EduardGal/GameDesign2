using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour {

    [SerializeField]
    private RoomLayoutGroup _roomLayout;
    private RoomLayoutGroup RoomLayout
     
    {
        get { return _roomLayout; }
    }

    public GameObject currentRoom;

    public void OnClickJoinRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {
            currentRoom.SetActive(true);
        }
        else
        {
            print("Join room failed.");
        }
    }
}
