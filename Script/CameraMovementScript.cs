using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Speed at which the camera follows
    public Vector3 offset; // Offset position from the player
    
    // Camera bounds
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        if (player != null) // Check if the player is assigned
        {
            // Define the desired position with the offset
            Vector3 desiredPosition = player.position + offset;

            // Clamp the desired position to stay within the bounds
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // Smoothly interpolate between the camera's current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}
