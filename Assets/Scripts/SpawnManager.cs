using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject objectBoss;

    GameObject player;
    
    int index;
    int stockEnem = 20;
    bool boss = true;
    float delay = 10;
    

    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("Player");
        
        InvokeRepeating("SpawnEnemies", 0.5f, 1f);
    }

    void Update()
    {
        
    }

    void SpawnEnemies(){
        if(player != null) {
            if(stockEnem > 0){
                Instantiate(enemy, spawnPoints[index].position, Quaternion.identity);
                stockEnem--;
            }else if (boss){
                if(Time.time > delay){
                   Instantiate(objectBoss, spawnPoints[4].position, Quaternion.identity);
                   boss = false;   
                }
                
            }

        
        }
    }
}
