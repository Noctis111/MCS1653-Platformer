using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Assign the Player object in the Inspector
    public Vector3 offset = new Vector3(0, 0, -15);  // Adjust for desired distance
    public float smoothSpeed = 5f; // Controls how smoothly the camera follows

    public GameObject bg0;
    public float bg0speed = 0.8f;
    public GameObject bg1;
    public float bg1speed = 0.6f;
    public GameObject bg2;
    public float bg2speed = 0.4f;

    private Vector3 previousPosition;

    void Start()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
            previousPosition = transform.position;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            Vector3 delta = smoothPosition - transform.position;

            transform.position = smoothPosition;

            // Apply parallax effect
            if (bg0 != null) bg0.transform.position += delta * bg0speed;
            if (bg1 != null) bg1.transform.position += delta * bg1speed;
            if (bg2 != null) bg2.transform.position += delta * bg2speed;
        }
    }
}
