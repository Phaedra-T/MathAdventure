using UnityEngine;
using System.Collections;

public class PipeEnter : MonoBehaviour{   
    [SerializeField] private AudioSource audioSource;
    public Transform exitPoint; // Target location to teleport the player
    public Vector3 enterDirection = Vector3.down; // Direction the player is "sucked" in
    public Vector3 exitDirection = Vector3.zero; // Exit direction (optional for animations)
    public float moveSpeed = 3f; // Speed of player "suction"
    public float transitionDuration = 0.5f; // Time taken to "suck" the player in

    public bool correctPipe;
    public static int c = 0; //wrong pipes counter

    void Start(){
        audioSource =  GetComponent<AudioSource>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the collider belongs to the Player
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Player entering pipe...");
            StartCoroutine(EnterPipe(other.transform));
        }
    }

    private IEnumerator EnterPipe(Transform player)
    {
        if(!correctPipe){
            c++;
        }
        // Disable player movement
        var playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Step 1: "Suck" player into the pipe
        Vector3 enteredPosition = transform.position + enterDirection;
        yield return MovePlayer(player, enteredPosition, Vector3.one, transitionDuration);
        yield return new WaitForSeconds(0.1f);
        audioSource.Play(); //play sound when entering pipe
        // Hide the player during teleportation
        SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
        if (playerRenderer != null)
        {
            playerRenderer.enabled = false; // Hide player
        }

        // Step 2: Instantly teleport player to the exit point
        if (exitDirection != Vector3.zero)
        {
            player.transform.position = exitPoint.position - exitDirection;
            yield return MovePlayer(player, exitPoint.position + exitDirection, Vector3.one, transitionDuration);
        }
        else
        {
            player.transform.position = exitPoint.position;
            player.transform.localScale = Vector3.one;
        }

        // Step 3: Delay and re-show the player
        // Short delay before reappearing
        if (playerRenderer != null)
        {
            playerRenderer.enabled = true; // Show player
        }

        // Re-enable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        Debug.Log("Player exited pipe.");
    }

    private IEnumerator MovePlayer(Transform player, Vector3 targetPosition, Vector3 targetScale, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = player.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Only interpolate the position
            player.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position
        player.position = targetPosition;
    }

    public static int  getWrongPipesCounter(){
        return c;
    }
}
