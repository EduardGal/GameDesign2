 using UnityEngine;
using UnityEngine.SceneManagement;
 
 public class TrigBush : MonoBehaviour
{
	 
     // Use this for initialization
     void Start () { 
		 
     }
     
     void OnTriggerEnter (Collider player)
     {
		 if(player.gameObject.tag == "PlayerOne")
		 {
			 
			 FindObjectOfType<AudioManager>().Play("Bush");
     }
 
    else if (player.gameObject.tag == "PlayerTwo")
        {
            FindObjectOfType<AudioManager>().Play("Bush");
        }
}
}
	 