using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<IReset> resettables = new List<IReset>();

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // Makes GameManager persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

public void RegisterResettable(IReset resettable)
{
    if (resettable == null)
    {
        Debug.LogWarning("Attempted to register a null resettable.");
        return;
    }

    if (!resettables.Contains(resettable))
    {
        resettables.Add(resettable);
        Debug.Log($"Successfully registered: {resettable}");
    }
    else
    {
        Debug.LogWarning($"Attempted to re-register: {resettable}");
    }
}

    public void ResetAll()
    {
        Debug.Log("ResetAll called. Resetting all registered objects.");
        for (int i = resettables.Count - 1; i >= 0; i--)
        {
            if (resettables[i] == null) // Check if the object is destroyed
            {
                Debug.LogWarning($"Removing destroyed object from resettables: {resettables[i]}");
                resettables.RemoveAt(i); // Remove it from the list
            }
            else
             try
            {
                resettables[i].ResetState();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error resetting object {resettables[i]}: {ex.Message}");
            }
        }

        FallingPlatformScript.fallCount = 0;
        PipeEnter.c = 0;
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene '{scene.name}' loaded. Clearing resettables list.");
        resettables.Clear();
    }

}
