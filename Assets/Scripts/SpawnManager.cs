using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject boss;
    GameObject player;
    public float timeBoss;
    bool bossContinue = true;
    int index;
    

    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("Player");
        index = Random.Range(0,spawnPoints.Length);
        InvokeRepeating("SpawnEnemies", 0.5f, 1f);
    }

    void Update()
    {
        timeBoss -= Time.deltaTime;

        if(player != null && timeBoss <= 0 && bossContinue){
            Instantiate(boss, spawnPoints[index].position, Quaternion.identity);
            bossContinue = false;
        }
    }

    void SpawnEnemies(){
        if(player != null) {
            Instantiate(enemy, spawnPoints[index].position, Quaternion.identity);
        }
    }
}
