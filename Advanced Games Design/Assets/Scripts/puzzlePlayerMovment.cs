using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzlePlayerMovment : MonoBehaviour
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
        CheckInput();
    }

    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        body.velocity = new Vector2(horizontal * 15, vertical * 15);
    }
}
