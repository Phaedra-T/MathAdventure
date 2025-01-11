using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum CollectibleType
{
    Strawberry, // Positive impact
    Pineapple   // Negative impact
}

public class Collectible2D : MonoBehaviour, IReset
{
    private Vector3 initialPosition;

    [SerializeField] private Animator animator;
    [SerializeField] private CollectibleType collectibleType; // Type of collectible
    [SerializeField] private AudioSource audioSource;
    private bool isCollected = false;
    

    private void Start()
    {
        SaveInitialState();
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.RegisterResettable(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            StartCoroutine(Collect());
            audioSource.Play();
            // Adjust the counter based on type
            if (collectibleType == CollectibleType.Pineapple)
            {
                CounterScript.instance.Dec(); // Decrease counter
            }
            else
            {
                CounterScript.instance.Inc(); // Increase counter
            }
        }
    }

    private IEnumerator Collect()
    {
        animator.SetBool("collected", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }

    public void SaveInitialState()
    {
        initialPosition = transform.position;
    }

    public void ResetState()
    {
        transform.position = initialPosition;
        gameObject.SetActive(true);
        isCollected = false;
        animator.SetBool("collected", false);
    }
}

