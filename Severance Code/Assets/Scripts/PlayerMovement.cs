using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 12.0f;
    public float dashSpeed = 25.0f;
    public float dashDuration = 0.3f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public Animator animator;

    private Rigidbody rb;
    private bool canDoubleJump;
    private bool canDash;
    private Transform playerModel;

    private bool isDashing = false;
    private float dashEndTime = 0f;
    private Vector3 dashDirection;

    public Collider attackCollider;
    public LayerMask enemyLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerModel = transform.GetChild(0);
        canDash = true; // Dash is available at start
    }

    void Update()
    {
        if (!isDashing)
        {
            Move();
            Jump();
        }

        HandleDash();
        HandleAttack();

        if (IsGrounded())
        {
            canDoubleJump = true;
            canDash = true; // Reset dash on landing
        }

        if (isDashing)
        {
            DashAttack();
        }
    }

    void Move()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(moveInputX * speed, rb.velocity.y, 0);
        rb.velocity = move;

        if (moveInputX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (moveInputX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
        else if (canDoubleJump && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            canDoubleJump = false;
            canDash = true; // Restore dash after double jump
        }
    }

    void HandleDash()
    {
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartDash();
        }

        if (isDashing && Time.time >= dashEndTime)
        {
            EndDash();
        }
    }

    void StartDash()
    {
        isDashing = true;
        canDash = false; // Disable further dashes
        dashEndTime = Time.time + dashDuration;

        float dashInputX = Input.GetAxisRaw("Horizontal");
        float dashInputY = Input.GetAxisRaw("Vertical");

        dashDirection = new Vector3(
            dashInputX != 0 ? dashInputX : (transform.rotation.y == 0 ? 1 : -1),
            dashInputY,
            0
        ).normalized;

        rb.velocity = dashDirection * dashSpeed;
        rb.useGravity = false;
    }

    void EndDash()
    {
        isDashing = false;
        rb.useGravity = true;
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapBox(attackCollider.bounds.center, attackCollider.bounds.extents, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy") && attackCollider.bounds.Contains(enemy.transform.position))
            {
                Destroy(enemy.gameObject);
                Debug.Log("Enemy Eliminated: " + enemy.name);
            }
        }
    }

    void DashAttack()
    {
        Collider[] hitEnemies = Physics.OverlapBox(attackCollider.bounds.center, attackCollider.bounds.extents, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy") && attackCollider.bounds.Contains(enemy.transform.position))
            {
                Destroy(enemy.gameObject);
                Debug.Log("Enemy Eliminated by Dash: " + enemy.name);
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
