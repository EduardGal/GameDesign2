using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.GlobalIllumination;

public class syncColor : Photon.MonoBehaviour
{
    public Light spotlight;
    public void Awake()
    {
        spotlight = GetComponent<Light>();
    }

    
}
