using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void NextDialogue(){
        dialogue[cont].SetActive(false);
        cont++;
        if(cont < 2){
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
}
