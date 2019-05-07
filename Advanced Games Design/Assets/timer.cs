using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{

    float timerCount = 60;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "60";
    }

    // Update is called once per frame
    void Update()
    {
        timerCount -= 1 * Time.deltaTime;

        GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(timerCount).ToString() ;


        if(timerCount <= 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
