 using UnityEngine;
using UnityEngine.SceneManagement;
 
 public class TrigCricket : MonoBehaviour
{
	 
     // Use this for initialization
     void Start () { 
		 
     }
     
     void OnTriggerEnter (Collider player)
     {
		 if(player.gameObject.tag == "PlayerOne")
		 {
			 
			 FindObjectOfType<AudioManager>().Play("Cricket");
     }
 
    else if (player.gameObject.tag == "PlayerTwo")
        {
            FindObjectOfType<AudioManager>().Play("Cricket");
        }
}
}
	 