using UnityEngine;

public class changeCharacter : MonoBehaviour
{
    public Avatar avatar_playerOne, avatar_playerTwo;
    //public UnityEditor.Animations.AnimatorController anim_playerOne, anim_playerTwo;
    public GameObject network;
    public GameObject sisterOne, sisterTwo;
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    // Start is called before the first frame update
    void Update()
    {
        network = GameObject.FindGameObjectWithTag("PlayerNetwork");
        if (network.GetComponent<playerNetwork>().hostIsPlayerOne)
        {
            Debug.LogWarning("CHANGING CHARACTER");
            Animator animator = gameObject.GetComponent<Animator>();
            if (PhotonNetwork.player.IsMasterClient)
            {
                sisterOne.SetActive(true);
                GetComponent<Animator>().avatar = avatar_playerOne;
                this.GetComponent<Animator>().runtimeAnimatorController = anim1 as RuntimeAnimatorController;

            }
            if (!PhotonNetwork.player.IsMasterClient)
            {
                sisterTwo.SetActive(true);
                GetComponent<Animator>().avatar = avatar_playerTwo;
                this.GetComponent<Animator>().runtimeAnimatorController = anim2 as RuntimeAnimatorController;

            }             
        }
        if (!network.GetComponent<playerNetwork>().hostIsPlayerOne)
        {
            Debug.LogWarning("CHANGING CHARACTER");
            Animator animator = gameObject.GetComponent<Animator>();
            if (!PhotonNetwork.player.IsMasterClient)
            {
                sisterOne.SetActive(true);
                GetComponent<Animator>().avatar = avatar_playerOne;
                this.GetComponent<Animator>().runtimeAnimatorController = anim1 as RuntimeAnimatorController;
            }
            if (PhotonNetwork.player.IsMasterClient)
            {
                sisterTwo.SetActive(true);
                GetComponent<Animator>().avatar = avatar_playerTwo;
                this.GetComponent<Animator>().runtimeAnimatorController = anim2 as RuntimeAnimatorController;
            }

            if (GetComponent<Animator>().avatar != null)
            {
                Destroy(this);
            }
        }
    }


}
