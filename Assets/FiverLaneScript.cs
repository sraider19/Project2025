using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ControlsScript manages lane switching and jump controls.
// - Four invisible lanes are defined via an array of X positions.
// - Left/right arrow keys trigger smooth lateral transitions between lanes.
// - The jump (Space key) is enabled only when the player is within a designated jump zone (tag "JumpZone").
// - An additional jump force is applied if the player is on an upward slope.
// Attach this script to the Player GameObject.
public class FiverLaneScript : MonoBehaviour
{
    // Array of lane X positions (adjust these to match your road layout).
    public float[] lanePositions = new float[] { -4.5f, -1.5f, 1.5f, 4.5f };

    // Variables to track lane state.
    private int targetLane = 1; // Starting lane index (can be changed as needed).

    // Lateral movement parameters.
    public float lateralSpeed = 5f; // Speed at which the player moves sideways to the target lane.

    // Start is called before the first frame update.
    void Start()
    {
        
    }

    // Update is called once per frame.
    void LateUpdate()
    {
        // --- Lane Switching Controls ---
        // Listen for left/right arrow key presses to change lanes.
        if (Input.GetKeyDown(KeyCode.N))
        {
            // Move to a lane to the left if not already in the leftmost lane.
            if (targetLane > 0)
            {
                targetLane--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            // Move to a lane to the right if not already in the rightmost lane.
            if (targetLane < lanePositions.Length - 1)
            {
                targetLane++;
            }
        }

        // Smoothly move the player's X position towards the target lane.
        Vector3 currentPosition = transform.position;
        float desiredZ = lanePositions[targetLane]; // The X position for the target lane.
        }
    }
