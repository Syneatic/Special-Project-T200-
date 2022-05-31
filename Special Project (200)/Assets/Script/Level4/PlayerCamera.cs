using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX = 100f;
    public float sensY = 100f;

    [SerializeField] Transform cam;
    [SerializeField] WallRun wallRun;
    public Transform orientation;
    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void MyInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        xRotation -= mouseY * sensY * multiplier;
        yRotation += mouseX * sensX * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();

        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);


    }
}
