using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;
    public Transform orientation;

    [Header("Pathways")]
    public PathWays ways;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    public SwitchWorld switchWorld;
    bool yes = false;

    [Header("Tuto Texts")]
    public GameObject helpText;
    public GameObject useText;

    [Header("Glasses")]
    public GameObject glasses;
    public GameObject glassesTrigger;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

        rb.drag = groundDrag;
        if (yes)
        {
            GetGlasses();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "WrongPath")
        {
            print("non");
            rb.transform.position = new Vector3(-3, 0, 115);
            ways.GenerateRightPathway();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HelpCube")
        {
            helpText.SetActive(true);
            yes = true;
        }
    }

    void GetGlasses()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            helpText.SetActive(false);
            useText.SetActive(true);
            Destroy(glasses);
            Destroy(glassesTrigger);
            switchWorld.canUse = true;
            yes = false;
        }
    }
}
