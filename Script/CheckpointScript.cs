using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool hasTouched = false; // Prevents retriggering the checkpoint
    public Animator animator; // Reference to the Animator component
    private CounterScript cs; // Reference to the CounterScript
    public EndLevelPopup endLevelPopup;
    private int deaths = 0;
    private int fallCount = 0;
    private int wrongAnswer = 0;
    void Start()
    {
        // Find the CounterScript instance in the scene
        cs = CounterScript.instance;
        audioSource =  GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player and the checkpoint hasn't been triggered yet
        if (other.CompareTag("Player") && !hasTouched)
        {
           if(SceneManager.GetActiveScene().name == "Level1"){// Ensure the CounterScript is available and check the count
                if (cs != null && cs.getCount() == 10)
                {
                    hasTouched = true; // Prevent re-triggering
                    animator.SetBool("hasTouched", true); // Play the animation
                    deaths = RespawnScript.getDeaths();
                    endLevelPopup.ShowPopup(deaths);
                    audioSource.Play();
                    
                }
            }else if(SceneManager.GetActiveScene().name == "Level2"){
                animator.SetBool("hasTouched", true);
                fallCount = FallingPlatformScript.getFallCount();
                endLevelPopup.ShowPopup(fallCount);
                audioSource.Play();
            }else if(SceneManager.GetActiveScene().name == "Level3"){
                wrongAnswer = PipeEnter.getWrongPipesCounter();
                endLevelPopup.ShowPopup(wrongAnswer);
                audioSource.Play();
            }
        }

    }
}
