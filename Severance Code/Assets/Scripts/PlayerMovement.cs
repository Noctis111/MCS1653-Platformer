using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 12.0f;
    public LayerMask groundLayer;
    public Transform groundCheck; // Empty GameObject at the bottom of the player
    public float groundCheckRadius = 0.2f;
    public Animator animator;

    private Rigidbody rb;
    private bool canDoubleJump; // Tracks only second jump
    private Transform playerModel; // Reference to the 3D model of the player

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerModel = transform.GetChild(0); // Assuming the 3D model is a child of the player object
    }

    void Update()
    {
        // Movement handling
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(moveInput * speed, rb.velocity.y, 0);
        rb.velocity = move;

        // Rotate the entire player object based on movement direction
        if (moveInput < 0)
        {
            // Rotate the entire player object to face left (180 degrees rotation)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            // Reset rotation to default (face right)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Jump()
    {
        if (IsGrounded())
        {
            canDoubleJump = true; // Reset double jump when on the ground
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
        else if (canDoubleJump && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            canDoubleJump = false; // Disable further jumps after second jump
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
