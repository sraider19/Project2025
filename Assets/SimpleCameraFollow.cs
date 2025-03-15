using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target; // Drag your character's GameObject here in the Inspector
    public Vector3 offset; // Set the desired offset (e.g., (0, 2, -5) for behind and above)
    public float smoothSpeed = 0.125f; // Adjust for smoother movement
    public float lookingSpeed = 0.125f;

    void Update()
    {
        // Calculate the desired camera position
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Ensure the camera always looks at the target (optional, but often desired)
        // transform.LookAt(target);
        Vector3 targetPostition = new Vector3(target.position.x, transform.position.y, target.position.z);

        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(target.transform.rotation, targetRotation, lookingSpeed * Time.deltaTime);
        //Quaternion.Euler(0, targetYAngle, 0)
    }
}