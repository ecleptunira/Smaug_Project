using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMan : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject objectBoss;

    GameObject player;
    
    int index;
    public int stockEnem;
    public int stockEnemB;
    bool boss = true;
    float delay = 25;

    

    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("Player");
        boss = false;
        InvokeRepeating("SpawnEnemies", 0.5f, 1f);
    }

    void Update()
    {  
        if(stockEnem <=0 && stockEnemB > 0){
            Invoke("SpawnEnemiesB", 10f);
        }

        if(stockEnemB <=0 && !boss){
            boss = true;
            Invoke("Boss", 10f);
        }
    }

    void SpawnEnemies(){
        if(player != null) {
            if(stockEnem > 0){
                int index = Random.Range(0, 5);
                Instantiate(enemy, spawnPoints[index].position, Quaternion.identity);
                stockEnem--;
            }
        
        }
    }

    void SpawnEnemiesB(){
        if(player != null) {
            if(stockEnemB > 0){
                int index = Random.Range(0, 5);
                Instantiate(enemy2, spawnPoints[index].position, Quaternion.identity);
                stockEnemB--;
            }
        
        }
    }

    void Boss(){
        Instantiate(objectBoss, spawnPoints[3].position, Quaternion.identity);
    }
}
