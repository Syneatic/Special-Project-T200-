using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject90 : MonoBehaviour
{
    public GameObject mainCube;
    public bool f1, f2, f3, f4, f5, f6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player" && f1 == true)
        {
            mainCube.GetComponent<RotateCube>().RotateF1();
        }

        if (other.tag == "Player" && f2 == true)
        {
            mainCube.GetComponent<RotateCube>().RotateF2();
        }
    }
}
