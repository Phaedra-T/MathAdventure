using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int dest;
    public float walkSpeed;

    public Transform player;
    public bool isChasing;
    public float detectRadius;
    public float chaseSpeed;

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set Collision Detection to Continuous for more accurate collision handling
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        // Check if player is within detection radius
        if (!isChasing && Vector2.Distance(transform.position, player.position) < detectRadius)
        {
            isChasing = true; // Start chasing
        }
        else if (isChasing && Vector2.Distance(transform.position, player.position) > detectRadius)
        {
            isChasing = false; // Stop chasing if player is out of range
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            //chasing
             Vector2 direction = (player.position - transform.position).normalized;
             rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

             // Flip the enemy sprite to face the player
             if (direction.x > 0 && transform.localScale.x < 0)
             {
                flip();
             }
            else if (direction.x < 0 && transform.localScale.x > 0)
            {
                flip();
            }
        }
        else
        {
            //patrol
            Vector2 direction = (patrolPoints[dest].position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * walkSpeed, rb.velocity.y);

            // Flip the sprite to face the patrol direction
            if (direction.x > 0 && transform.localScale.x < 0)
            {
                flip();
            }
            else if (direction.x < 0 && transform.localScale.x > 0)
            {
                flip();
            }

            // Switch destination point if close to the current target
            if (Vector2.Distance(transform.position, patrolPoints[dest].position) < 0.3f)
            {

                dest = (dest + 1) % patrolPoints.Length;
            }
        }
    }

    // Function to flip the enemy sprite
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Visualize the patrol points and detection radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(patrolPoints[0].transform.position, 0.1f);
        Gizmos.DrawWireSphere(patrolPoints[1].transform.position, 0.1f);
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
