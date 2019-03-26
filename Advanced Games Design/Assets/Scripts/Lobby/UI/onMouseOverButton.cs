using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class onMouseOverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Image img;
    
  
    private void Awake()
    {
       img = gameObject.GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.enabled = false;

    }
}
