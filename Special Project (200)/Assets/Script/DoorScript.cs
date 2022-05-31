using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private Transform pressurePlate;

    [SerializeField] private float speed;
    [SerializeField] private float timer;
    [SerializeField] private float cooldown;
    [SerializeField] private float timerStart;
    [SerializeField] private bool isStep = false;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 goalsPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isStep == true && timer <= 2 && timer >= 0)
        {
            door.position = Vector3.Lerp(door.position, goalsPos, speed * Time.deltaTime);
            
        }
        else
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime; 
            }
            else
            {
                door.position = Vector3.Lerp(door.position, startPos, speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isStep = true;
            timer = cooldown;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isStep = false;
        }
    }
}
