using UnityEngine;

public class CameraFollowHook : MonoBehaviour
{
    public Transform hook;            // Reference to the hook's transform
    public Vector3 offset;            // Offset from the hook's position
    public float followSpeed = 5f;    // Speed at which the camera follows the hook

    private float fixedXPosition;     // Fixed x position to lock the camera horizontally

    void Start()
    {
        hook = GameObject.Find("Hook").transform;
        fixedXPosition = transform.position.x;  // Store the initial x position of the camera
    }

    void LateUpdate()
    {
        if (hook != null)
        {
            // Calculate the desired position of the camera based on the hook's position and offset
            Vector3 targetPosition = hook.position + offset;

            // Use the fixed x position for horizontal stability
            targetPosition.x = fixedXPosition;

            // Smoothly interpolate the camera's position to the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
