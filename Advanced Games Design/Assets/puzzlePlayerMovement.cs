using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzlePlayerMovement : Photon.MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    public bool devTesting;
    // Start is called before the first frame update
    void Awake()
    {
       body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!devTesting)
        {
            if (photonView.isMine)
            {
                CheckInput();
            }
        }
        else CheckInput();
    }

    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        body.velocity = new Vector2(horizontal * 40, vertical * 40);
    }
}
