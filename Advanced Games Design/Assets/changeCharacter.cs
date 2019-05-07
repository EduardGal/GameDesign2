using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class changeCharacter : MonoBehaviour
{
    public Avatar avatar_playerOne, avatar_playerTwo;
    public AnimatorController anim_playerOne, anim_playerTwo;
    private GameObject network;
    // Start is called before the first frame update
    void Start()
    {
        network = GameObject.FindGameObjectWithTag("PlayerNetwork");
        if (network.GetComponent<playerNetwork>().hostIsPlayerOne)
        {
            if (PhotonNetwork.player.IsMasterClient)
            {
                GetComponent<Animator>().avatar = avatar_playerOne;
                GetComponent<Animator>().runtimeAnimatorController = anim_playerOne;
            }
            if (!PhotonNetwork.player.IsMasterClient)
            {
                GetComponent<Animator>().avatar = avatar_playerTwo;
                GetComponent<Animator>().runtimeAnimatorController = anim_playerTwo;
            }


            if (!network.GetComponent<playerNetwork>().hostIsPlayerOne)
            {
                if (!PhotonNetwork.player.IsMasterClient)
                {
                    GetComponent<Animator>().avatar = avatar_playerOne;
                    GetComponent<Animator>().runtimeAnimatorController = anim_playerOne;
                }
                if (PhotonNetwork.player.IsMasterClient)
                {
                    GetComponent<Animator>().avatar = avatar_playerTwo;
                    GetComponent<Animator>().runtimeAnimatorController = anim_playerTwo;
                }
            }




        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
