using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeRotation : MonoBehaviour
{
    public float raycastDistance = 0.5f; // Adjust this to suit your character's size
    public float maxSlopeAngle = 45f;
    public LayerMask groundMask;
    private float _slopeAngle;

    public RaycastHit hit;

    private bool isOnSlope()
    {
        Debug.DrawRay(transform.position + Vector3.forward, Vector3.up * raycastDistance, Color.green, groundMask);

        //RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, raycastDistance))
        {

            if (Vector3.Angle(Vector3.up, hit.normal) < maxSlopeAngle)
            {
                _slopeAngle = Vector3.Angle(Vector3.up, hit.normal);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void FixedUpdate()
    {
        if (isOnSlope())
        {
            //Get the slope normal, use it to find a rotation to the slope's normal
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation;

            //Apply rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); 
            // Adjust 10f to your desired rotation speed.
        }
        
    }
}