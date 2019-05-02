﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : Photon.MonoBehaviour
{
    public PhotonView[] players;
    [SerializeField] float walkSpeed = 2.0f;
    [SerializeField] float runSpeed = 6.0f;
    [SerializeField] float gravity = -12.0f;
    [SerializeField] float rotationSmoothTime = 0.2f;
    [SerializeField] float speedSmoothTime = 0.05f;

    public bool devTesting = false;
    private PhotonView PhotonView;
    public GameObject plCam;
    private Vector3 selfPos;
    private Quaternion realRotation;
    private GameObject myInvCanvas;
    private bool tagSet = false;


    private float playerSpeed, animSpeedPercent, turnSmoothVelocity, speedSmoothVelocity, currentSpeed, velocityY;

    Animator animator;
    CharacterController characterController;
    Transform cameraTransform;

    
    private void Awake()
    {
        if (devTesting)
        {
            plCam.SetActive(true);
        }
        PhotonView = GetComponent<PhotonView>();
        if (!devTesting && PhotonView.isMine)
        {
            plCam.SetActive(true);

            if (tagSet == false)
            {
                if (GetComponent<PhotonView>().viewID.ToString().Contains("1"))
                {
                    gameObject.transform.tag = "PlayerOne";
                    gameObject.name = "PlayerOne";
                    tagSet = true;
                }
                
                else if (GetComponent<PhotonView>().viewID.ToString().Contains("2"))
                {
                    gameObject.transform.tag = "PlayerTwo";
                    gameObject.name = "PlayerTwo";
                    tagSet = true;
                }
            }
                
        }

        


    }


    void Start()
    {
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        FindObjectOfType<AudioManager>().Play("OpenInventory");
        PhotonView.RPC("Tags", PhotonTargets.AllBufferedViaServer, null);
        
    }



    private void Update()
    {

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (!GameObject.FindGameObjectWithTag("PlayerOne"))
            {
                GameObject.FindGameObjectWithTag("Player").tag = "PlayerOne";
            } else GameObject.FindGameObjectWithTag("Player").tag = "PlayerTwo";
        }
        if (!devTesting)
        {
            if (photonView.isMine)
            {
                CheckInput();
            }
            else SmoothNetMovement();
        }
        else CheckInput();

    }



    void CheckInput()
    {
        // player inputs
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = playerInput.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);

        PlayerMovementAndRotation(inputDirection, running);

        // animations
        animSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f);
        animator.SetFloat("speedPercent", animSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

    void PlayerMovementAndRotation(Vector2 inputDirection, bool running)
    {
        if (inputDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, rotationSmoothTime);
        }

        playerSpeed = ((running) ? runSpeed : walkSpeed) * inputDirection.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, playerSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        characterController.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

        if (characterController.isGrounded)
        {
            velocityY = 0;
        }
    }
    private void SmoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
        transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, Time.deltaTime * 8);
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //THIS IS OUR PLAYER
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(animator.GetFloat("speedPercent"));

        }
        else
        {
            //THIS IS OTHER COOP PLAYER
            selfPos = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();

            animator.SetFloat("speedPercent", (float)stream.ReceiveNext());
        }
    }
}