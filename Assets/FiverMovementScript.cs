using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MovementScript automatically moves the player forward with acceleration,
// allows gradual braking, and performs a raycast to measure distance ahead.
// Attach this script to the Player GameObject.
public class FiverMovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController characterController;
    // Public variables for tweaking movement in the Inspector.
    public float acceleration = 2f;       // Rate at which the player speeds up.
    public float maxSpeed = 20f;          // Maximum forward speed.
    public float deceleration = 5f;       // Rate at which the player slows down when braking.
    public float rayDistance = 50f;       // Maximum distance for the forward raycast.

    // Private variable to keep track of the current forward speed.
    private float currentSpeed = 0f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame.
    void Update()
    {
        // Check if the brake key is held down.
        // Here we use the "B" key to trigger braking.
        bool isBraking = Input.GetKey(KeyCode.B);

        if (isBraking)
        {
            // Gradually reduce speed by deceleration value.
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed < 0f)
            {
                currentSpeed = 0f; // Prevent negative speed.
            }
        }
        else
        {
            // Gradually increase speed by acceleration value.
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed; // Clamp speed to maxSpeed.
            }
        }

        // Move the player forward along its local Z-axis.
        //transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(-moveSpeed, 0, 0).normalized;

        // Apply gravity
        // Move the character
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = moveSpeed;
        characterController.SimpleMove(forward * curSpeed);

        // --- Raycast to measure distance ahead ---
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;   // Start from the player's position.
        Vector3 rayDirection = transform.forward;   // Cast forward along the player's facing.

        // If the ray hits an object within the specified distance...
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance))
        {
            // Draw a red line from the player to the hit point in the Scene view.
            Debug.DrawLine(rayOrigin, hit.point, Color.red);
            // Log the distance for debugging purposes.
            Debug.Log("Distance to object: " + hit.distance);
        }
        else
        {
            // If nothing is hit, draw a green line showing the full ray distance.
            Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * rayDistance, Color.green);
        }
    }
}
