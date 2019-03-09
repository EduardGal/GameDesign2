using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fadeOut : MonoBehaviour
{

    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        img.color = Color.Lerp(new Color(255, 255, 255, 255), new Color(255, 255, 255, 0), 10 * Time.deltaTime);
    }
}
