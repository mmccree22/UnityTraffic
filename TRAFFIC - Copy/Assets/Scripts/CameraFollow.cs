using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // Reference to the target object (car)
    public Vector3 offset = new Vector3(0f, 10f, -10f);  // Offset of the camera from the target
    public float damping = 1f;       // Smoothing factor for camera movement

    private Vector3 desiredPosition; // Desired position of the camera
    private Vector3 velocity = Vector3.zero;  // Velocity for smooth damping

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position of the camera based on the target's position and offset
            desiredPosition = target.position + offset;

            // Use SmoothDamp to smoothly move the camera towards the desired position
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, damping);
        }
    }
}
