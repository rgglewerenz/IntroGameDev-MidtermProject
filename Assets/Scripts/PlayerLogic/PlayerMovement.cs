using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float impulse = 5f;
    [SerializeField]
    public float maxSpeed = 10f;
    [SerializeField]
    public float rotationSpeed = 90f;

    public ThrustHandler thrustHandler;

    private Vector3 momentum = Vector3.zero;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            if(rb.velocity.magnitude < maxSpeed)
            {
                thrustHandler.Emit();
                rb.AddForce(transform.forward * impulse * Time.deltaTime, ForceMode.Impulse);
            }
        }
        else {
            thrustHandler.StopEmitting();
        }


        if (Input.GetKey(KeyCode.S))
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * -1 * impulse * Time.deltaTime, ForceMode.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * -rotationSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotationSpeed);
        }
        
        
    }
}
