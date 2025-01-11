using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{

   public void LoadScene(String sceneName){
        SceneManager.LoadScene(sceneName);

        GameManager.Instance.ResetAll();
        Debug.Log("Level Restarted!");
   }

   public void QuitGame(){
      Application.Quit();
      Debug.Log("Application quit");
   }
}
