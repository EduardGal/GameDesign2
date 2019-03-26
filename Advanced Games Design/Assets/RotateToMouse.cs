using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour

  
{
    public Vector3 mousePos;
    
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        float AngleRad = Mathf.Atan2(this.gameObject.transform.position.y - mousePos.y, this.gameObject.transform.position.x - mousePos.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg + 92);
    }
}
