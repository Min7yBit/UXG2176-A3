using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 2f;
    public Transform cameraHolder;

    private Rigidbody rb;
    private float rotationX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent physics from rotating the player
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        RotatePlayer();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * moveX + transform.forward * moveZ;
        Vector3 targetVelocity = moveDir.normalized * moveSpeed;
        Vector3 velocityChange = targetVelocity - rb.linearVelocity;
        velocityChange.y = 0; // Don't modify vertical speed
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Rotate player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        cameraHolder.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
