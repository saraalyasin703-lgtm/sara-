using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 6f;
    public float horizontalSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 7f;

    [Header("Slide")]
    public float slideDuration = 0.6f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.3f;

    private Rigidbody rb;
    private CapsuleCollider col;
    private bool isGrounded;
    private bool isSliding;

    float standHeight;
    Vector3 standCenter;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        // حفظ الوضع الطبيعي
        standHeight = col.height;
        standCenter = col.center;
    }

    void Update()
    {
        MoveForward();
        MoveHorizontal();

        CheckGround();
        HandleJump();
        HandleSlide();
    }

    // -------------------------
    // Movement
    // -------------------------

    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void MoveHorizontal()
    {
        float input = ReadHorizontalInput();
        transform.Translate(Vector3.right * input * horizontalSpeed * Time.deltaTime);
    }

    float ReadHorizontalInput()
    {
        if (Keyboard.current == null) return 0f;

        float right =
            (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) ? 1f : 0f;

        float left =
            (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) ? 1f : 0f;

        return right - left;
    }

    // -------------------------
    // Ground + Jump
    // -------------------------

    void CheckGround()
    {
        if (col == null)
        {
            isGrounded = false;
            return;
        }

        Vector3 origin = col.bounds.center;
        float rayLength = col.bounds.extents.y + groundCheckDistance;

        isGrounded = Physics.Raycast(origin, Vector3.down, rayLength, groundLayer);
    }

    void HandleJump()
    {
        if (Keyboard.current == null) return;
        if (!Keyboard.current.spaceKey.wasPressedThisFrame) return;
        if (!isGrounded || isSliding) return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // -------------------------
    // Slide
    // -------------------------

    void HandleSlide()
    {
        if (Keyboard.current == null) return;
        if (!Keyboard.current.leftCtrlKey.wasPressedThisFrame) return;
        if (!isGrounded || isSliding) return;

        StartCoroutine(Slide());
    }

    IEnumerator Slide()
    {
        isSliding = true;

        // تصغير جسم اللاعب
        col.height = standHeight * 0.5f;
        col.center = standCenter * 0.5f;

        yield return new WaitForSeconds(slideDuration);

        // رجوع للوضع الطبيعي
        col.height = standHeight;
        col.center = standCenter;

        isSliding = false;
    }
}


