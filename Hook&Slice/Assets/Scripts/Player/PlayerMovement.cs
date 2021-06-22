using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform orientation;

    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private bool isSprinting;
    
    [SerializeField]
    private float moveSpeed = 6f;
    [SerializeField]
    private float movementMultiplier = 10f;
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float rbDrag = 6f;
    [SerializeField]
    private float playerHeight = 2f;

    private float horMov;
    private float verMov;

    [Header("Ground Detection")]
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private bool isGrounded;
    private float groundDist = 2f;

    RaycastHit slopeHit;
    
    private Rigidbody playerRB;

    private Vector3 moveDir;
    private Vector3 slopeDir;

    // Start is called before the first frame update
    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerInput();
        ControlDrag();

        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDist, groundMask);

        print(isGrounded);

        if (Input.GetKey(KeyCode.LeftShift))
            isSprinting = true;
        else
            isSprinting = false;

        if (isSprinting)
            moveSpeed = 15f;

        if (!isSprinting)
            moveSpeed = 6f;

        slopeDir = Vector3.ProjectOnPlane(moveDir, slopeHit.normal);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void PlayerInput()
    {
        horMov = Input.GetAxisRaw("Horizontal");
        verMov = Input.GetAxisRaw("Vertical");

        moveDir = orientation.forward * verMov + orientation.right * horMov;
    }

    private void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            playerRB.AddForce(moveDir.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            playerRB.AddForce(slopeDir.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
    }

    private void ControlDrag()
    {
        playerRB.drag = rbDrag;
    }

}
