 using UnityEngine;
using UnityEngine.SceneManagement;
 
 public class TrigObjectives : MonoBehaviour
{
     public GameObject uiObject;
	 
     // Use this for initialization
     void Start () { 
		 
     uiObject.SetActive(false);
     }
     
     void OnTriggerEnter (Collider player)
     {
		 if(player.gameObject.tag == "PlayerOne")
		 {
			 
	uiObject.SetActive(true);
			 FindObjectOfType<AudioManager>().Play("Objective");
     }
 
    else if (player.gameObject.tag == "PlayerTwo")
        {
            uiObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Objective");
        }
}
}
	 