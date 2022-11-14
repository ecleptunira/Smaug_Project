using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
   
    GameObject player;
    
    int index;
    

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
            index = Random.Range(0,spawnPoints.Length);
            Instantiate(enemy, spawnPoints[index].position, Quaternion.identity);
        }
    }
}
