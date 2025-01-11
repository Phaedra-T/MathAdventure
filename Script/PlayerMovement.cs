using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpingPower = 10f;
    private bool isFacingRight = true;
    private bool doubleJump;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    private void Update()
    {
        // Capture horizontal input
        horizontal = Input.GetAxisRaw("Horizontal");

        // Set the Speed parameter for running animation
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsDoubleJumping", false);
        }

        // Handle Jump and Double Jump logic
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                // First Jump
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                animator.SetBool("IsJumping", true);
            }
            else if (!doubleJump)
            {
                // Double Jump
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                doubleJump = true;
                animator.SetBool("IsDoubleJumping", true);
            }
        }

        // Reduce upward velocity if jump button is released early
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        // Check if the player is on the ground
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    private void Flip()
    {
        // Flip the player's sprite when changing directions
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
