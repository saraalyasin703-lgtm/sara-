using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 7f;                 // السرعة الابتدائية
    public float speedIncreasePerSecond = 0.5f;     // زيادة السرعة مع الوقت
    public float maxForwardSpeed = 20f;              // الحد الأقصى للسرعة
    public float horizontalSpeed = 10f;
    public float xClamp = 3.5f;

    [Header("Jump")]
    public float jumpForce = 6f;

    [Header("Slide")]
    public float slideDuration = 0.5f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    Rigidbody rb;
    CapsuleCollider col;

    bool isGrounded;
    bool isSliding;

    float standHeight;
    Vector3 standCenter;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        // منع الانحراف (الميلان)
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        standHeight = col.height;
        standCenter = col.center;
    }

    void Update()
    {
        // زيادة السرعة مع الوقت
        forwardSpeed = Mathf.Min(
            forwardSpeed + speedIncreasePerSecond * Time.deltaTime,
            maxForwardSpeed
        );

        CheckGround();
        HandleJump();
        HandleSlide();
    }

    void FixedUpdate()
    {
        MoveForward();
        MoveHorizontal();
    }

    // -------------------------
    // Movement
    // -------------------------
    void MoveForward()
    {
        Vector3 v = rb.linearVelocity;
        v.z = forwardSpeed;
        rb.linearVelocity = v;
    }

    void MoveHorizontal()
    {
        float input = 0f;

        if (Keyboard.current != null)
        {
            float right = (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) ? 1f : 0f;
            float left = (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) ? 1f : 0f;
            input = right - left;
        }

        Vector3 pos = rb.position;
        pos.x += input * horizontalSpeed * Time.fixedDeltaTime;
        pos.x = Mathf.Clamp(pos.x, -xClamp, xClamp);

        rb.MovePosition(pos);
    }

    // -------------------------
    // Ground + Jump
    // -------------------------
    void CheckGround()
    {
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

        col.height = standHeight * 0.5f;
        col.center = new Vector3(standCenter.x, standCenter.y * 0.5f, standCenter.z);

        yield return new WaitForSeconds(slideDuration);

        col.height = standHeight;
        col.center = standCenter;

        isSliding = false;
    }
}
