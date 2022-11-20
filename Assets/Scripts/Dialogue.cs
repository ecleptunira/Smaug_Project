using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject[] image;
   
    
    int cont = 0;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void Next(){
        image[cont].SetActive(false);
        cont++;

        if(cont < 5){

            image[cont].SetActive(true);

        }else{

            Time.timeScale = 1;
            image[5].SetActive(false);
            
        }
        
        
    }
}
