using System.Collections;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour, IReset
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fallDelay;
    [SerializeField] private float destroyDelay;
    [SerializeField] private float fallGravityScale = 5f;
    
    private Vector3 initialPosition;
    private RigidbodyType2D initialBodyType;
    private bool falling = false;

    public static int fallCount = 0;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        SaveInitialState();
        audioSource =  GetComponent<AudioSource>();
        GameObject.FindObjectOfType<GameManager>().RegisterResettable(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (falling) return; // Avoid re-triggering fall if already falling

        if (collision.transform.CompareTag("Player"))
        {
            fallCount++;
            audioSource.Play();
            StartCoroutine(StartFall());
        }
    }

    private IEnumerator StartFall()
    {
        falling = true;
        yield return new WaitForSeconds(fallDelay);
        rb.gravityScale = fallGravityScale;

        // Start falling by changing the body type
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Wait for the platform to fall before resetting
        yield return new WaitForSeconds(destroyDelay);
    }

    public void SaveInitialState()
    {
        initialPosition = transform.position;
        initialBodyType = rb.bodyType;
    }

    public void ResetState()
    {
        // Log to ensure ResetState is being called
        Debug.Log("Resetting platform to initial position and state");

        // Reset the position and body type
        transform.position = initialPosition;
        rb.bodyType = initialBodyType;
        
        // Clear any residual velocity and angular velocity
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Reset the falling flag so the platform can fall again when triggered
        falling = false;
    }

    public static int getFallCount(){
        return fallCount;
    }
}
