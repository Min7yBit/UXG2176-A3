using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float acceleration = 12f;
    public float deceleration = 10f;
    public float airControl = 0.4f;

    [Header("Camera Look")]
    public float lookSensitivity = 2f;
    public Transform cameraHolder;

    [Header("Jump")]
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public float groundCheckDistance = 1.1f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private float rotationX;
    private bool canMove = true;
    public bool CanMove { get => canMove; set => canMove = value; }

    // ---------------------------
    // INPUT STORAGE
    // ---------------------------
    private float inputX;
    private float inputZ;
    private bool jumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!CanMove) return;

        // -----------------------
        // GATHER INPUT HERE
        // -----------------------
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            jumpPressed = true;

        RotatePlayer();
    }

    void FixedUpdate()
    {
        if (!CanMove) return;

        MovePlayer();
        HandleJump();
    }

    // -------------------------------------------------------
    // MOVEMENT
    // -------------------------------------------------------
    void MovePlayer()
    {
        Vector3 inputDir = (transform.right * inputX + transform.forward * inputZ).normalized;

        Vector3 currentVel = rb.linearVelocity;
        Vector3 horizontalVel = new Vector3(currentVel.x, 0, currentVel.z);

        float control = IsGrounded() ? 1f : airControl;

        if (inputDir.sqrMagnitude > 0.01f)
        {
            // Accelerate toward desired velocity
            Vector3 target = inputDir * moveSpeed;
            Vector3 newVel = Vector3.MoveTowards(
                horizontalVel,
                target,
                acceleration * control * Time.fixedDeltaTime
            );

            rb.linearVelocity = new Vector3(newVel.x, currentVel.y, newVel.z);
        }
        else
        {
            // Decelerate naturally when no input
            Vector3 newVel = Vector3.MoveTowards(
                horizontalVel,
                Vector3.zero,
                deceleration * control * Time.fixedDeltaTime
            );

            rb.linearVelocity = new Vector3(newVel.x, currentVel.y, newVel.z);
        }
    }

    // -------------------------------------------------------
    // JUMPING
    // -------------------------------------------------------
    void HandleJump()
    {
        if (jumpPressed && IsGrounded())
        {
            Vector3 vel = rb.linearVelocity;
            vel.y = 0;
            rb.linearVelocity = vel;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }

        jumpPressed = false; // reset after applying
    }

    // -------------------------------------------------------
    // GROUND CHECK
    // -------------------------------------------------------
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    // -------------------------------------------------------
    // CAMERA LOOK
    // -------------------------------------------------------
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawWireSphere(transform.position + Vector3.down * groundCheckDistance, 0.05f);
    }
}
