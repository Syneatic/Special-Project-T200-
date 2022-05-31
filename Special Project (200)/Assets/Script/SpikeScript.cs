using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    [SerializeField] private Transform spike;
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
        if (isStep == true)
        {
            if (timer <= timerStart && timer >= 0)
            {
                spike.position = Vector3.Lerp(spike.position, goalsPos, speed * Time.deltaTime);
            }

            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                isStep = false;
            }
        }
        else
        {
            spike.position = Vector3.Lerp(spike.position, startPos, speed * Time.deltaTime);
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
}
