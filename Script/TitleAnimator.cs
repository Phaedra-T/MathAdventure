using UnityEngine;
using TMPro;

public class TitleAnimator : MonoBehaviour
{
    public float floatSpeed = 1f; // Speed of floating animation
    public float floatAmplitude = 5f; // How much it moves up and down

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Save starting position
    }

    void Update()
    {
        // Float the title up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
