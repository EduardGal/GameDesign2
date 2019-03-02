using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NetworkReference
{
    public class networkManager : Photon.MonoBehaviour
    {
        public PhotonView[] players;
        public GameObject playerOne, playerTwo;
        // Start is called before the first frame update



        public void Update()
        {
            if (playerOne == null)
            {

                players = UnityEngine.Object.FindObjectsOfType<PhotonView>();

                foreach (PhotonView pView in players)
                {
                    if (pView.gameObject.tag.Contains("PlayerOne"))
                    {
                        playerOne = pView.gameObject;
                    }

                }
            }
            if (playerTwo == null)
            {

                    players = UnityEngine.Object.FindObjectsOfType<PhotonView>();

                    foreach (PhotonView pView in players)
                    {
                        if (pView.gameObject.tag.Contains("PlayerTwo"))
                        {
                            playerTwo = pView.gameObject;
                        }

                    }
            }
        }

    }
}
    

