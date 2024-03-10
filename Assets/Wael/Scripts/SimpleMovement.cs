using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed;
    public float sprintSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        MyInput();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    void MyInput()
    {
        //Assuming this is 3rd-person movement and the default Input Manager configuration is used.
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //Calculate movement direction 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.MovePosition(transform.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
    }
}
