using System.Collections;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    [SerializeField] private float force = 10f; // Strength of the fan
    [SerializeField] private float onTime = 3f; // Duration the fan stays on
    [SerializeField] private float offTime = 2f; // Duration the fan stays off
    [SerializeField] private Animator fanAnimator; // Animator for visual feedback

    private bool isOn = true; // Whether the fan is currently on

    private void Start()
    {
        StartCoroutine(FanCycle());
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isOn && other.CompareTag("Player"))
        {
            // Apply an upward force to the player
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.up * force, ForceMode2D.Force);
            }
        }
    }

    private IEnumerator FanCycle()
    {
        while (true)
        {
            // Turn the fan on
            isOn = true;
            if (fanAnimator != null) fanAnimator.SetBool("isOn", true);
            yield return new WaitForSeconds(onTime);

            // Turn the fan off
            isOn = false;
            if (fanAnimator != null) fanAnimator.SetBool("isOn", false);
            yield return new WaitForSeconds(offTime);
        }
    }
}
