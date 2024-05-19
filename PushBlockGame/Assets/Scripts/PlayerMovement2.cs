using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private PushBlockSettings m_PushBlockSettings;

    void Start()
    {
        m_PushBlockSettings = FindObjectOfType<PushBlockSettings>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input from user
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Calculate movement and rotation
        Vector3 moveDirection = transform.forward * moveInput * m_PushBlockSettings.agentRunSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnInput * turnSpeed * Time.fixedDeltaTime, 0f);

        // Apply movement and rotation
        if (moveInput != 0)
        {
            var dirToGo = transform.forward * 1f;
            rb.AddForce(dirToGo * m_PushBlockSettings.agentRunSpeed, ForceMode.VelocityChange);
        }
        rb.MoveRotation(rb.rotation * turnRotation);

        // Update camera position and rotation
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position - transform.forward * 5f + Vector3.up * 2f; // Adjust the offset as needed
            cameraTransform.LookAt(transform.position + transform.forward * 2f); // Adjust the look-at point as needed
        }
    }
}
