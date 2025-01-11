using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePin : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            // Start the collection process
            StartCoroutine(Collect());
        }
    }

    private IEnumerator Collect()
    {
        // Set the collected animation to true
        animator.SetBool("collected", true);

        // Wait for the animation to finish before destroying the object
        // You can adjust this time or use animation events to make it more precise
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destroy the collectible after the animation completes
        Destroy(gameObject);
    }
}
