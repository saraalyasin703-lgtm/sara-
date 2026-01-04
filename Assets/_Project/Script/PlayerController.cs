using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim; //عشان الانيميشن
    public float forwardSpeed = 6f;
    public float horizontalSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody rb;

    void Start()
    {
        // GetComponent
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Forward movement
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Horizontal movement (GetAxis)
        float x = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * x * horizontalSpeed * Time.deltaTime);

        // Jump (AddForce)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
    }

}