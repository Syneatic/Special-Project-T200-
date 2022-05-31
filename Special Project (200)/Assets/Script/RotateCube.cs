using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RotateF1()
    {
        transform.Rotate(0, 0, 0);
    }

    public void RotateF2()
    {
        transform.Rotate(90, 0, 0);
    }
}
