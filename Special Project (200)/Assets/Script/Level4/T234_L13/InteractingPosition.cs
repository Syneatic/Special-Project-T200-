using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingPosition : MonoBehaviour
{
    [SerializeField] private float lookSensitivity = 500f;

    float multiplier = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * multiplier;

        transform.Rotate(Vector3.up * mouseX);
    }
}
