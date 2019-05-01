using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateWithMouse : MonoBehaviour
{
    public float speed = 5f;

    public GameObject photonScript;
    
    // Start is called before the first frame update
    void Start()
    {
        photonScript.GetComponent<mainMenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //direction.x = 10;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("mouse");
            print(transform.eulerAngles);
            if (transform.eulerAngles.z > 69 && transform.eulerAngles.z < 169)
            {
                print("a");
                photonScript.GetComponent<mainMenuManager>().OnJoin();
            }
            if (transform.eulerAngles.z > 210 && transform.eulerAngles.z < 300)
            {
                print("a");
                photonScript.GetComponent<mainMenuManager>().OnCreate();
            }
        }
        
    }
}
