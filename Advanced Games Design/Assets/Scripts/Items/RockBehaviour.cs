using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    Rigidbody rb;
    SphereCollider sphereCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Perhaps let the rock roll a little then freeze contraints?
            rb.constraints = RigidbodyConstraints.FreezeAll;
            
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Ground")
    //    {
    //        Debug.Log("Ground identified");
    //        //Perhaps let the rock roll a little then freeze contraints?
    //        rb.constraints = RigidbodyConstraints.FreezeAll;
    //    }
    //}
}
