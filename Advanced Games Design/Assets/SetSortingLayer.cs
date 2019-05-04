using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSortingLayer : MonoBehaviour
{
    public Renderer MyRenderer;
    public string MySortingLayer;
    public int MySortingOrderInLayer;

    // Use this for initialization
    void Start()
    {
        if (MyRenderer == null)
            MyRenderer = this.GetComponent<Renderer>();
        StartCoroutine(WaitForSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (MyRenderer == null)
            MyRenderer = this.GetComponent<Renderer>();
        MyRenderer.sortingLayerName = MySortingLayer;
        MyRenderer.sortingOrder = MySortingOrderInLayer;

      
    }

    public IEnumerator WaitForSpawn()
    {
        this.gameObject.GetComponent<TrailRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<TrailRenderer>().enabled = true;
    }
}

