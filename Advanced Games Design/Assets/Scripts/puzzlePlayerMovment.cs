using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzlePlayerMovment : Photon.MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    public bool devTesting;
    public Vector3 startPos;
    public GameObject triggerObj;


    // Start is called before the first frame update

    void Awake()
    {
        

        body = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<TrailRenderer>().sortingLayerName = ("Defualt");
    }
    private void Start()
    {

        if (!devTesting)
        {
            if (PhotonNetwork.player.IsMasterClient)
            {
                photonView.TransferOwnership(PhotonNetwork.player);
            }
        }
        if (devTesting && !PhotonNetwork.player.IsMasterClient)
        {
            photonView.TransferOwnership(PhotonNetwork.player);
        }
        transform.position = startPos;
    }
    // Update is called once per frame
    void Update()
    {

        if (gameObject.GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0))
        {
            Instantiate(triggerObj, this.gameObject.transform.position, Quaternion.identity);
        }
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
