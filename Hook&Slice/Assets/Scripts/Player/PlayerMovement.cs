using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private bool isSprinting = false;
    [SerializeField]
    private bool isGrounded = true;
    [SerializeField]
    private float moveSpeed = 6f;
    [SerializeField]
    private float movementMultiplier = 10f;
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float rbDrag = 6f;

    private float horMov;
    private float verMov;

    private LayerMask groundMask;

    private Rigidbody playerRB;

    private Vector3 moveDir;

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

        if (Input.GetKey(KeyCode.LeftShift))
            isSprinting = true;
        else
            isSprinting = false;

        if (isSprinting)
            moveSpeed = 15f;

        if (!isSprinting)
            moveSpeed = 6f;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        horMov = Input.GetAxisRaw("Horizontal");
        verMov = Input.GetAxisRaw("Vertical");

        moveDir = transform.forward * verMov + transform.right * horMov;
    }

    private void MovePlayer()
    {
        playerRB.AddForce(moveDir.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
    }

    private void ControlDrag()
    {
        playerRB.drag = rbDrag;
    }

}
