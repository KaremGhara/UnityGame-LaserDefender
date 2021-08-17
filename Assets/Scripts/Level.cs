using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
   
   
  public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
      
    }
    

    public void LoadMainGame()
    {
        SceneManager.LoadScene("SpaceGame");

    }
   
    public void LoadGameOver()
    {
        
       
        StartCoroutine(DelayMainMenu());
    }
    IEnumerator DelayMainMenu()
    {
       
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Game Over");


    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
