using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class SimpleCameraController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float mouseSensitivity = 0.3f;
    public float jumpForce = 2.0f;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private Rigidbody rb;
    private CapsuleCollider col;
    private bool isGrounded;

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        rotationY += lookInput.x * mouseSensitivity;
        rotationX -= lookInput.y * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 40f);
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

        CheckGrounded();

        if (isGrounded && jumpAction.WasPressedThisFrame())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

    void FixedUpdate()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector3 forward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 right = new Vector3(transform.right.x, 0, transform.right.z).normalized;

        Vector3 moveDirection = (forward * moveInput.y) + (right * moveInput.x);

        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        Vector3 rayStart = transform.position + col.center;
        if (Physics.SphereCast(rayStart, col.radius * 0.9f, moveDirection, out RaycastHit hit, 0.5f))
        {
            if (Vector3.Angle(Vector3.up, hit.normal) > 45f)
            {
                moveDirection = Vector3.ProjectOnPlane(moveDirection, hit.normal).normalized;
            }
        }

        float currentY = rb.linearVelocity.y;
        if (!isGrounded && currentY > jumpForce)
        {
            currentY = jumpForce;
        }

        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, currentY, moveDirection.z * moveSpeed);
    }

    private void CheckGrounded()
    {
        float rayLength = (col.height / 2f) + 0.1f;
        Vector3 rayStart = transform.position + col.center;
        isGrounded = Physics.Raycast(rayStart, Vector3.down, rayLength);
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}