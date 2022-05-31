using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{
    [Header("FPV Character Setup")]
    public CharacterController characterController;
    public Transform firstPersonCamera;
    public Transform groundDetector;

    public LayerMask groundMask;

    [Header("Movement Parameters")]
    public float speed = 5f;
    public float gravity = -30f;
    public float jumpHeight = 1.0f;
    public float groundDistance = 0.4f;
    public float lookSensitivity = 200f;


    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;
    //public bool doublejump = true;


    public Vector3 move;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        CharacterMove();
        MouseLook();
    }
    void CharacterMove()
    {
        // Checking if the FPV Player in on ground
        isGrounded = Physics.CheckSphere(groundDetector.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Detecting WASD Keyboard inputs
        float x = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // Applying the movements
        move = transform.right * x + transform.forward * v;

        characterController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }


        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
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

    public Transform Cube;
    public float RotationSpeed = 20f;
    Vector3 f2up = new Vector3(0, 0, 0);
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "f1")
        {

        }
        if (other.tag == "f2")
        {
            Debug.Log("Rotating f2");
            Physics.gravity = new Vector3(0, -1.0F, 0);
            Cube.localEulerAngles = new Vector3(Mathf.LerpAngle(0, 0, 0), 0, 0);
        }
        else if (other.tag == "f3")
        {

        }
        else if (other.tag == "f4")
        {

        }
        else if (other.tag == "f5")
        {

        }
        else if (other.tag == "f6")
        {

        }

    }

}