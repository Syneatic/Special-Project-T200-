using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("FPV Character Setup")]
    public Rigidbody rb;
    public Transform firstPersonCamera;
    public Transform groundDetector;
    public Transform WallDetector;
    public Transform orientation;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public float movementMultiplier = 10f;

    public float horizontalMovement;
    public float verticalMovement;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [Header("Movement Parameters")]
    public float speed = 100f;
    public float gravity = -50f;
    public float jumpHeight = 100f;
    private bool isGrounded;
    private float xRotation = 0f;
    private bool isJumpPad;
    private bool isLava;
    private bool isWall;
    private bool _shouldJump;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 7f;
    [SerializeField] float acceleration = 7f;

    [Header("Camera")]
    public float lookSensitivity = 200f;

    [Header("Crouching")]
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if the FPV Player in on ground
        isGrounded = Physics.CheckSphere(groundDetector.position, groundDistance, groundMask);
    }

    void FixedUpdate()
    {
        ControlMovement();

        JumpControl();

        ControlSpeed();

        //  Movement speed when player on ground
        if (isGrounded == true)
        {
            rb.AddForce(moveDirection.normalized * speed * movementMultiplier, ForceMode.Acceleration);
        }

        MouseLook();
    }
    

    void ControlMovement()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void JumpControl()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }

        rb.AddForce(transform.up * gravity);
    }

    void CrouchControl()
    {
        //Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    private void StartCrouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
    }

    private void StopCrouch()
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    void ControlSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        firstPersonCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
