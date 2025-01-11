using UnityEngine;

public interface IReset
{
    void SaveInitialState();  // Method to save the initial state
    void ResetState();        // Method to reset to the initial state
}
