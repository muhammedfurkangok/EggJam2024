using UnityEngine;

public class MouseLookCamera : MonoBehaviour
{
    public Transform player;       
    public float sensitivity = 100f;
    public float distanceFromPlayer = 3f;
    public Vector2 rotationLimits = new Vector2(-30f, 60f);

    private float mouseX;
    private float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        mouseX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Clamp vertical rotation
        mouseY = Mathf.Clamp(mouseY, rotationLimits.x, rotationLimits.y);

        // Rotate the camera around the player
        transform.position = player.position - transform.forward * distanceFromPlayer;
        transform.LookAt(player);

        // Apply rotation
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
    }
}
