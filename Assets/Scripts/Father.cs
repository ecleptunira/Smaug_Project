using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : MonoBehaviour
{
    
    [SerializeField] Transform[] spawnPoints;
    
    [SerializeField] GameObject periquito;
    [SerializeField] GameObject father;
    
GameObject player;
    
   
    
    
    bool character = false;
    

    

    void Start()
    {  
      player = GameObject.FindGameObjectWithTag("Player"); 
      if(!character){
        character = true;
          //  Invoke("Character", 1f);
          Instantiate(periquito, spawnPoints[0].position, Quaternion.identity);
            Instantiate(father, spawnPoints[1].position, Quaternion.identity);
       }
       
        
    }

    void Update()
    {
       
    }

    void Character(){
      
            Instantiate(periquito, spawnPoints[0].position, Quaternion.identity);
            Instantiate(father, spawnPoints[1].position, Quaternion.identity);
        
    }
}
