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


    public void Update()
    {
        if (transform.parent.GetChild(transform.parent.childCount -1) == this.transform)
        {
            if (transform.GetChild(0).gameObject.active == false && transform.GetChild(1).gameObject.active == false)
            {
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                
            }
        }
        else
        {
            if (transform.GetChild(0).gameObject.active == true && transform.GetChild(1).gameObject.active == true)
            {
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                
            }
        }
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
