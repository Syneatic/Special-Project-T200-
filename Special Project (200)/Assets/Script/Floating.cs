using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    [SerializeField]
    private float walkingSpeed;

    [SerializeField]
    private float mouseSensitivity;

    private bool onCorner = false;
    private bool onEdge = false;

    private Transform edgeTransform;
    private Transform cornerTransform;

    private bool isGrounded = false;

    [SerializeField]
    private float jumpForce = 5f;

    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Controls of the player (Moving Left, Right, forward, Backwards)
        Vector3 velocity;
        velocity = transform.forward * Input.GetAxis("Vertical") * walkingSpeed + transform.right * Input.GetAxis("Horizontal") * walkingSpeed;

        //Controls the player to jump
        Physics.gravity = -transform.up * 9.8f;

        //velocity += currentGravity;


        transform.position += velocity * Time.deltaTime;


        if (Input.GetButtonDown("Jump")) rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);


        if (onEdge)
        {
            WhileOnEdge();
        }
        else if (onCorner)
        {
            WhileOnCorner();
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0), Space.Self);

    }

    //
    private void WhileOnEdge()
    {
        Vector3 player2Edge = edgeTransform.position - transform.position;

        Vector3 playerUpDirection;

        Vector3 playerLeftDirection;
        Vector3 playerForwardDirection;


        if (edgeTransform.position.x == 0)
        {
            playerUpDirection = (
                Vector3.Dot(player2Edge, edgeTransform.right) * edgeTransform.right
                - player2Edge).normalized;

            playerLeftDirection = Vector3.Cross(transform.forward, playerUpDirection);
            playerForwardDirection = Vector3.Cross(playerUpDirection, playerLeftDirection);

            transform.rotation = Quaternion.LookRotation(playerForwardDirection, playerUpDirection);

        }
        else if (edgeTransform.position.y == 0)
        {
            playerUpDirection = (
                Vector3.Dot(player2Edge, edgeTransform.up) * edgeTransform.up

                - player2Edge).normalized;

            playerLeftDirection = Vector3.Cross(transform.forward, playerUpDirection);
            playerForwardDirection = Vector3.Cross(playerUpDirection, playerLeftDirection);

            transform.rotation = Quaternion.LookRotation(playerForwardDirection, playerUpDirection);

        }
        else if (edgeTransform.position.z == 0)
        {
            playerUpDirection = (
                Vector3.Dot(player2Edge, edgeTransform.forward) * edgeTransform.forward
                - player2Edge).normalized;

            playerLeftDirection = Vector3.Cross(transform.forward, playerUpDirection);
            playerForwardDirection = Vector3.Cross(playerUpDirection, playerLeftDirection);

            transform.rotation = Quaternion.LookRotation(playerForwardDirection, playerUpDirection);

        }
    }

    private void WhileOnCorner()
    {
        Vector3 playerUpDirection = (transform.position - cornerTransform.position).normalized;

        Vector3 playerRightDirection = Vector3.Cross(playerUpDirection, transform.forward);

        if (playerRightDirection == Vector3.zero) playerRightDirection = transform.right;

        Vector3 playerForwardDirection = Vector3.Cross(playerRightDirection, playerUpDirection);

        transform.rotation = Quaternion.LookRotation(playerForwardDirection, playerUpDirection);
    }

    private void OnTriggerEnter(Collider other)
    {

        print("Entered " + other.name);
        if (other.CompareTag("Edge"))
        {
            onEdge = true;
            edgeTransform = other.transform;
        }

        if (other.CompareTag("Corner"))
        {
            onCorner = true;
            cornerTransform = other.transform;
        }

        if (other.CompareTag("Surface"))
        {
            onEdge = false;
            onCorner = false;

            Vector3 playerForward;

            switch (other.name)
            {
                case ("Surface1"):
                    playerForward = Vector3.Cross(Vector3.up, -transform.right);
                    transform.rotation = Quaternion.LookRotation(playerForward, Vector3.up);
                    break;

                case ("Surface2"):
                    playerForward = Vector3.Cross(Vector3.forward, -transform.right);
                    transform.rotation = Quaternion.LookRotation(playerForward, Vector3.forward);
                    break;

                case ("Surface3"):
                    playerForward = Vector3.Cross(Vector3.right, -transform.right);
                    transform.rotation = Quaternion.LookRotation(playerForward, Vector3.right);
                    break;

                case ("Surface4"):
                    playerForward = Vector3.Cross(-Vector3.forward, -transform.right);
                    transform.rotation = Quaternion.LookRotation(playerForward, -Vector3.forward);
                    break;

                case ("Surface5"):
                    playerForward = Vector3.Cross(-Vector3.right, -transform.right);
                    transform.rotation = Quaternion.LookRotation(playerForward, -Vector3.right);
                    break;

                case ("Surface6"):
                    playerForward = Vector3.Cross(-Vector3.up, -transform.right);
                    transform.rotation = Quaternion.LookRotation(playerForward, -Vector3.up);
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Edge"))
        {
            onEdge = false;
        }

        if (other.CompareTag("Corner"))
        {
            onCorner = false;
        }
    }
}
