using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverController : MonoBehaviour
{
    public GameObject gameOver;
    // Start is called before the first frame update
    public void Quit(){
        Application.Quit();
    }

    public void Replay(){
        
        gameOver.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
        Time.timeScale = 1f; 
    }
}
