using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastDialogue : MonoBehaviour
{
    public GameObject[] dialogue;
    public GameObject gameOver;
    public GameObject gameWin;
    private Transform target;
    int cont = 0;
    
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        Time.timeScale = 0;
    }

    public void NextDialogue(){
        dialogue[cont].SetActive(false);
        cont++;
        if(cont < dialogue.Length){
            dialogue[cont].SetActive(true);
            
        }
        
    }

    
    public void ShowScreenGame(){
        if(target != null){
            gameWin.SetActive(true);
        }else{
            gameOver.SetActive(true);
        }
    }

    public void Scene2(){
        SceneManager.LoadScene("Scene2");
    }
}
