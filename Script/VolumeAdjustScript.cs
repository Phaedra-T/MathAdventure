using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjustScript : MonoBehaviour
{
    public Slider volumeSlider;      // Reference to the Slider
    public AudioSource audioSource; // Reference to the AudioSource

    void Start()
    {
        // Ensure the slider starts at the current audio source volume
        if (volumeSlider != null && audioSource != null)
        {
            volumeSlider.value = audioSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // Function to set the volume
    public void SetVolume(float value)
    {
        AudioListener.volume = value; // Adjusts global volume
    }
}
