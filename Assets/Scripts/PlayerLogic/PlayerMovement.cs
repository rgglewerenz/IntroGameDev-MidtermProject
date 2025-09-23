using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 5f;
    [SerializeField]
    public float rotationSpeed = 90f;

    private Vector3 momentum = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            momentum += (Vector3.forward * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            momentum += (Vector3.back * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * -rotationSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotationSpeed);
        }
        if (momentum.magnitude > 0.01f)
        {
            transform.Translate(momentum, Space.Self);
            momentum = Vector3.Lerp(momentum, Vector3.zero, 0.5f);
        }
        else
        {
            momentum = Vector3.zero;
        }
    }
}
