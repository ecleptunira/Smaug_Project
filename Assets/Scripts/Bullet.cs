using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
   
    public int doDamage;

    

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.right * Time.deltaTime * speed); 
       Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<Enemy>().DamageEnemy(doDamage);
           
            Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("Boss")){
            collision.gameObject.GetComponent<EnemyControll>().DamageEnemy(doDamage);
           
            Destroy(this.gameObject);
        }
       
        
    }
}
