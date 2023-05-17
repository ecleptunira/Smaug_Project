using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject objectBoss;
    [SerializeField] GameObject periquito;
    [SerializeField] GameObject father;
    

    GameObject player;
   
    
    int index;
    public int stockEnem;
    bool boss = true;
    bool emit = true;
    float delay = 25;
    bool character;
    int stockBullet = Player.stockBulletBoss;

    

    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("Player");
      
        boss = false;
        character = false;
        emit = false;
        InvokeRepeating("SpawnEnemies", 0.5f, 1f);
        

    }

    void Update()
    {
        if(stockEnem <=0 && !boss){
            boss = true;
            Invoke("Boss", 5f);
        }
       
        
        // if(!EnemyControll.isAlive){
        //     emit = true;
        // }
      
        // if(boss){
        //     if(objectBoss == null){
                
        //         Invoke("Character", 3f);
        //     }
           
            
        // }
        
        
        
        
    
        //     // if (boss && !character){
        //     //      character = true;
        //     //     Invoke("Character", 3f);
        //     // }
        
        
         
        
        
        
        
    }

    

    void SpawnEnemies(){
        if(player != null) {
            if(stockEnem > 0){
                int index = Random.Range(0, spawnPoints.Length-1);
                Instantiate(enemy, spawnPoints[index].position, Quaternion.identity);
                stockEnem--;
            }
        
        }
    }

    void Boss(){
        
        Instantiate(objectBoss, spawnPoints[4].position, Quaternion.identity);
      
        
        
    }

    void Character(){
        if(!character){
            character = true;
            Instantiate(periquito, spawnPoints[5].position, Quaternion.identity);
            Instantiate(father, spawnPoints[6].position, Quaternion.identity);
        }
        
    }
    

    
}
