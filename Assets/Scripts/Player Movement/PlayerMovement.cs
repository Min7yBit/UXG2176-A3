using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 2f;
    public float jumpForce = 7f;
    public Transform cameraHolder;

    private Rigidbody rb;
    private float rotationX;

    // Ground check
    public float groundCheckDistance = 1.1f;
    public LayerMask groundLayer;

    private bool canMove = true;
    public bool CanMove { get => canMove; set => canMove = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent physics from rotating the player
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if(!CanMove)
        {
            return;
        }

        MovePlayer();
        HandleJump();
    }

    void Update()
    {
        if (!CanMove)
        {
            return;
        }

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

    void HandleJump()
    {
        if (IsGrounded() && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
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

    // -----------------------------
    // GIZMO TO SHOW GROUNDCHECK
    // -----------------------------
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        // Draw the ground-check line
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);

        // Draw a small sphere where the ray ends
        Gizmos.DrawWireSphere(transform.position + Vector3.down * groundCheckDistance, 0.05f);
    }
}
