using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Assign the Player object in the Inspector
    public Vector3 offset = new Vector3(0, 0, -15);  // Adjust for desired distance
    public float smoothSpeed = 5f; // Controls how smoothly the camera follows

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, player.position.z) + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothPosition;
        }
    }
}
