using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzlePlayerMovment : Photon.MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    public bool devTesting;
    public TrailRenderer trail;
    public bool PlayerOne;
    // Start is called before the first frame update

    void Awake()
    {
        if (PlayerOne)
        {
            photonView.TransferOwnership(1001);
        }else photonView.TransferOwnership(2001);



        body = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        
        trail.sortingLayerName = "Background";
        trail.sortingOrder = 2;
    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        body.velocity = new Vector2(horizontal * 15, vertical * 15);
    }
}
