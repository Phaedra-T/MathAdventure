using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndLevelPopup : MonoBehaviour, IReset
{
    public GameObject star1; 
    public GameObject star2; 
    public GameObject star3; 
    private int c; 

    private void Start()
    {
        if(GameManager.Instance!= null){
            GameManager.Instance.RegisterResettable(this);
            Debug.Log($"EndLevelPopup registered with GameManager.");
        }
          
        ResetState();
    }
    public void ShowPopup(int counter)
    {
        c = counter;
        gameObject.SetActive(true); // Show the popup
        DisplayStars();
    }

     private void DisplayStars()
     {
         Debug.Log("Counter: " + c);
        

        // Display hearts based on death count
        star1.SetActive(c <= 3);
        star2.SetActive(c < 2);
        star3.SetActive(c == 0);

        Debug.Log("Heart1 Active: " + star1.activeSelf);
        Debug.Log("Heart2 Active: " + star2.activeSelf);
        Debug.Log("Heart3 Active: " + star3.activeSelf);
     }

    public void SaveInitialState()
    {
        throw new System.NotImplementedException();
    }

    public void ResetState()
    {
        c = 0;
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        gameObject.SetActive(false);
        Debug.Log($"Popup reset: c={c}, stars inactive");
    }
}
