using UnityEngine;

public class NewPlayerMovementScript : MonoBehaviour
{
    [Header("FPV Character Setup")]
    public Rigidbody rb;
    public Transform firstPersonCamera;
    public Transform groundDetector;
    public Transform LavaDetector;
    public Transform WallDetector;
    public Transform orientation;

    public LayerMask groundMask;
    public LayerMask jumpPadMask;
    public LayerMask whatIsWall;



    [Header("Movement Parameters")]
    public float speed = 100f;
    public float gravity = -50f;
    public float jumpHeight = 100f;
    public float groundDistance = 0.4f;
    public float wallDistance = 0.6f;
    public float lookSensitivity = 450f;
    public float playerHeight = 2f;
    public float movementMultiplier = 10f;
    public float airMultiplier = 0.1f;
    public float horizontalMovement;
    public float verticalMovement;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    private bool isGrounded;
    private float xRotation = 0f;
    private bool isJumpPad;
    private bool isWall;
    private bool _shouldJump;
    RaycastHit slopeHit;

    [Header("Crouching")]
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;


    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 7f;
    [SerializeField] float acceleration = 7f;

    [Header("Lava")]
    public Vector3 spawnLocation = new Vector3(-2.06f, 1.22f, 5.99f);

    [Header("JumpPad")]
    public float jumpPadForce = 300f;

    public GameObject NextLevelScreen;

    [Header("DoubleJump")]
    private float extraJumps;
    public float extraJumpsValue;
    public static bool isDoubleJump = false;

    public GameObject weapon;
    public static bool reachEnd = false;

    [Header("WallCling")]
    public float jumpforce = 300;
    public float minPower = 300;
    public float maxPower = 1000f;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 0.4f;

    //Input
    bool crouching;

    [Header("Collectables")]
    public static bool collectable1;
    public static bool collectable2;
    public static bool collectable3;

    private void Start()
    {
        extraJumps = extraJumpsValue;
        rb.freezeRotation = true;
        playerScale = transform.localScale;
    }

    void FixedUpdate()
    {

        MovePlayerOnSlope();
        ControlDrag();
        ControlSpeed();

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

        //  When player press 'Space'
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }

        //  Gravity being used
        rb.AddForce(transform.up * gravity);

        if (isJumpPad)
        {
            rb.AddForce(transform.up * jumpPadForce);
        }
    }

    void Update()
    {
        CharacterMove();
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void CharacterMove()
    {

        // Checking if the FPV Player in on ground
        isGrounded = Physics.CheckSphere(groundDetector.position, groundDistance, groundMask);

        //  Movement speed when player on ground
        if (isGrounded == true)
        {
            rb.AddForce(moveDirection.normalized * speed * movementMultiplier, ForceMode.Acceleration);
        }

        //  Movement speed when player in the air
        if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * speed * airMultiplier, ForceMode.Acceleration);
        }

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

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.2f))
        {
            if (slopeHit.normal != Vector3.up)
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

    void MovePlayerOnSlope()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * speed, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * speed, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * speed, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Portal")
        {
            NextLevelScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            reachEnd = true;
            weapon.SetActive(false);
        }

        if (other.tag == "collectable1")
        {
            Destroy(other.gameObject);
            collectable1 = true;
        }

        if (other.tag == "collectable2")
        {
            Destroy(other.gameObject);
            collectable2 = true;
        }

        if (other.tag == "collectable3")
        {
            Destroy(other.gameObject);
            collectable3 = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Portal")
        {
            reachEnd = false;
        }
    }
}

