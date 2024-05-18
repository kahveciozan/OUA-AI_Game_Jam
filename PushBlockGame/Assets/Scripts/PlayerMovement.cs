using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private PushBlockSettings m_PushBlockSettings;

    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        m_PushBlockSettings = FindObjectOfType<PushBlockSettings>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from user
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Store the movement vector
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed;
    }

    void FixedUpdate()
    {

        var dirToGo = transform.forward * 1f;
        var rotateDir = Vector3.zero;

        // Apply the movement vector to the rigidbody in FixedUpdate
        if (movement != Vector3.zero)
        {
            rb.AddForce(dirToGo * m_PushBlockSettings.agentRunSpeed  , ForceMode.VelocityChange);
                
            // Rotate to face the movement direction
            Quaternion newRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(newRotation);
        }
    }

}

