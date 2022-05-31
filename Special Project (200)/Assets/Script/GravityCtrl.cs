using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCtrl : MonoBehaviour
{
    public GravityOrbit Gravity;
    private Rigidbody Rb;

    public float RotationSpeed = 20;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Gravity)
        {
            Vector3 gravityUp = Vector3.zero;

            if (Gravity.FixedDirection)
            {
                gravityUp = Gravity.transform.up;
            }
            else
            {
                gravityUp = (transform.position - Gravity.transform.position).normalized;
            }

            Vector3 localUp = transform.up;

            Quaternion targetrotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;

            Rb.GetComponent<Rigidbody>().rotation = targetrotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, RotationSpeed * Time.deltaTime);


            Rb.AddForce((-gravityUp * Gravity.Gravity) * Rb.mass);
        }
    }
}