using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearMovement : MonoBehaviour
{
    public float speed = 10f;         // Forward movement speed
    public float turnSpeed = 50f;     // Turning speed
    public float acceleration = 2f;   // How fast the bear accelerates
    public float deceleration = 2f;   // How fast the bear decelerates naturally
    public float brakeStrength = 5f;  // How fast the bear brakes
    public float maxSpeed = 20f;      // Maximum speed the bear can reach
    private float currentSpeed = 0f;  // Current speed of the bear

    public Transform cameraTransform; // Assign the camera in the Inspector or in code
    public Vector3 cameraOffset = new Vector3(0, 5, -10);  // Offset of the camera from the bear

    void Update()
    {
        // Handle acceleration with W
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        // Handle braking with S
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed -= brakeStrength * Time.deltaTime;
        }
        else
        {
            // Deceleration when no key is pressed
            currentSpeed -= deceleration * Time.deltaTime;
        }

        // Clamp the speed to avoid going negative or above max speed
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        // Move the bear forward based on current speed
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Handle turning
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        // Update the camera position to follow the bear's movement and orientation
        // The camera stays behind the bear, rotating with it
        cameraTransform.position = transform.position + transform.TransformDirection(cameraOffset);

        // Optionally, make the camera look at the bear to keep focus on it
        cameraTransform.LookAt(transform.position + Vector3.up * 1.5f);  // Adjust Y offset if needed
    }
}
