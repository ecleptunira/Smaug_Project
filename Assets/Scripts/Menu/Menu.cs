using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{   
    public GameObject credits;


    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }

    public void Credits()
    {
        credits.SetActive(true);
        
    }

    public void CloseCredits()
    {
        credits.SetActive(false);
    }
}
