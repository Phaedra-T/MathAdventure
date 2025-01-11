using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Data.Common;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public GameObject player;
    public GameObject respawnPoint;
    public GameManager gameManager;

    public static int dc;

    void Start()
    {
        audioSource =  GetComponent<AudioSource>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        dc = 0;
    }

    private void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.CompareTag("Player")) {
            audioSource.Play();
            player.transform.position = respawnPoint.transform.position;
            gameManager.ResetAll(); // Reset the game state on respawn;
            dc++;
            Debug.Log($"dc = {dc}");
            if(SceneManager.GetActiveScene().name == "Level1"){
                CounterScript.instance.ResetCounter();
                
            }
            
                
        }
    }

    public static int getDeaths(){
        return dc;
    }
}
