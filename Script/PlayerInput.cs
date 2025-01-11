using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed;       // Horizontal movement speed
    public float jumpForce;   // Base jump force
    public float maxJumpTime = 0.5f; // Maximum time you can hold the jump
    public Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded = false; // Track if the player is grounded
    public Transform groundCheck;    // Reference to GroundCheck object
    public LayerMask groundLayer;    // Layer for ground objects

    private float jumpTimeCounter;   // Counter for jump time
    private bool isJumping = false;  // Track if jump button is being held

    void Start()
    {
        // Assign Animator and Rigidbody2D components
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get horizontal input (A/D or Left/Right arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal");

        // Set animator parameters based on input
        if (animator != null)
        {
            animator.SetFloat("moveX", Mathf.Abs(moveX));  // Use absolute value to avoid negative animation states
            animator.SetBool("isMoving", moveX != 0);
            animator.SetBool("isGrounded", isGrounded);
        }

        // Update velocity for horizontal movement
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (moveX != 0)
       // Flip sprite based on movement direction, preserving original scale



        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;  // Start jump
            jumpTimeCounter = 0; // Reset jump time counter
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Initial jump force

            // Set animator trigger for jump
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }

        if (Input.GetButton("Jump") && isJumping && jumpTimeCounter < maxJumpTime)
        {
            jumpTimeCounter += Time.deltaTime; // Increment jump time
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force while holding jump button
        }

        if (Input.GetButtonUp("Jump") || jumpTimeCounter >= maxJumpTime)
        {
            isJumping = false; // Stop jump when button is released or max jump time is reached
        }
    }

    void FixedUpdate()
    {
        // Check if the GroundCheck collider is touching any objects on the ground layer
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Reset jump trigger when landing
        if (isGrounded && !wasGrounded)
        {
            if (animator != null)
            {
                animator.ResetTrigger("Jump"); // Reset the jump trigger when landing
            }
        }
    }

    // Optional: Draw ground check radius in the editor for debugging
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        }
    }
}
