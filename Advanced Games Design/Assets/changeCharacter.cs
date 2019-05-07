using UnityEngine;

public class changeCharacter : MonoBehaviour
{
    public Avatar avatar_playerOne, avatar_playerTwo;
    //public UnityEditor.Animations.AnimatorController anim_playerOne, anim_playerTwo;
    private GameObject network;
    // Start is called before the first frame update
    void Start()
    {
        network = GameObject.FindGameObjectWithTag("PlayerNetwork");
        if (network.GetComponent<playerNetwork>().hostIsPlayerOne)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (PhotonNetwork.player.IsMasterClient)
            {
                GetComponent<Animator>().avatar = avatar_playerOne;

                animator.runtimeAnimatorController = Resources.Load("Assets/THEA'S NEW ANIMATIONS/Sister_1/Sister_1_StandardIdle_FIX.controller") as RuntimeAnimatorController;

            }
            if (!PhotonNetwork.player.IsMasterClient)
            {
                GetComponent<Animator>().avatar = avatar_playerTwo;
                animator.runtimeAnimatorController = Resources.Load("Assets/THEA'S NEW ANIMATIONS/Sister_2/Sister_2_StandardIdle_FIX.controller") as RuntimeAnimatorController;

            }


            if (!network.GetComponent<playerNetwork>().hostIsPlayerOne)
            {
                if (!PhotonNetwork.player.IsMasterClient)
                {
                    GetComponent<Animator>().avatar = avatar_playerOne;
                    animator.runtimeAnimatorController = Resources.Load("Assets/THEA'S NEW ANIMATIONS/Sister_1/Sister_1_StandardIdle_FIX.controller") as RuntimeAnimatorController;
                    if (PhotonNetwork.player.IsMasterClient)
                    {
                        GetComponent<Animator>().avatar = avatar_playerTwo;
                        animator.runtimeAnimatorController = Resources.Load("Assets/THEA'S NEW ANIMATIONS/Sister_2/Sister_2_StandardIdle_FIX.controller") as RuntimeAnimatorController;
                    }
                }




            }
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
